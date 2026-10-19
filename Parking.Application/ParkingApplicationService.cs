using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Parking.Application.Persistence;
using Parking.Core;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Models;

namespace Parking.Application;


/// Each window shares a single instance. The Controller does not handle EF entities; all write operations are revalidated within a transaction.
/// Amounts are expressed in integer Australian cents; entity timestamps are in Unix seconds; DTO timestamps are in UTC DateTime.
public sealed class ParkingApplicationService : IParkingApplicationService
{
    public const long MonthlyFeeCents = 20_000;
    private static long MonthlyFee(ParkingSpace space) => space.IsBig ? MonthlyFeeCents * 120 / 100 : MonthlyFeeCents;
    private readonly IDbContextFactory<ParkingDbContext> _factory;
    private readonly IClock _clock;
    private readonly ISpaceRecommendationService _recommendations;
    private readonly IFeePolicy _hourly = new HourlyFeePolicy();
    private readonly IFeePolicy _leased = new LeaseFeePolicy();
    private readonly object _gate = new();
    private bool _admin;
    private PendingCheckout? _pending;

    private sealed class PendingCheckout
    {
        public required CheckoutQuote Quote { get; init; }
        public ParkingRequest? Request { get; init; }
        public int SpaceId { get; init; }
    }
    private sealed class BusinessException(ErrorCode code, string message) : Exception(message)
    {
        public ErrorCode Code { get; } = code;
    }
    public ParkingApplicationService(IDbContextFactory<ParkingDbContext> factory,
        IClock? clock = null, ISpaceRecommendationService? recommendations = null)
    {
        _factory = factory;
        _clock = clock ?? new SystemClock();
        _recommendations = recommendations ?? new SpaceRecommendationService();
    }
    public bool IsAdministrator { get { lock (_gate) return _admin; } }
    public void LogoutAdministrator() { lock (_gate) _admin = false; }
    public void DiscardQuote(Guid quoteId)
    {
        lock (_gate) if (_pending?.Quote.QuoteId == quoteId) _pending = null;
    }

    private static void Require(bool condition, ErrorCode code, string message)
    {
        if (!condition) throw new BusinessException(code, message);
    }
    private long Now() => new DateTimeOffset(_clock.UtcNow).ToUnixTimeSeconds();
    private static DateTime Utc(long seconds) => DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
    private static string Plate(string? raw)
    {
        var normalized = (raw ?? "").Trim().Replace(" ", "").Replace("-", "").ToUpperInvariant();
        Require(Regex.IsMatch(normalized, "^[A-Z0-9]{2,8}$"), ErrorCode.InvalidPlate,
            "The license plate must consist of 2–8 letters or numbers (spaces and hyphens will be automatically removed).");
        return normalized;
    }
    private static ParkingRequest Normalize(ParkingRequest request)
    {
        Require(request != null, ErrorCode.InvalidInput, "Vehicle registration information is missing.");
        var r = request!.Copy();
        r.Plate = Plate(r.Plate);
        Require(Enum.IsDefined(r.Size), ErrorCode.InvalidInput, "Invalid vehicle model.");
        r.Name = (r.Name ?? "").Trim();
        r.Contact = (r.Contact ?? "").Trim();
        r.Address = (r.Address ?? "").Trim();
        if (r.RegisterLease)
            Require(r.Name.Length is > 0 and <= 100 && r.Contact.Length is > 0 and <= 200 &&
                r.Address.Length is > 0 and <= 300, ErrorCode.InvalidInput,
                "Monthly rental customers must provide their name (≤100), contact information (≤200), and address (≤300).");
        return r;
    }
    private static ParkingRequest TenantRequest(Lease lease) => new()
    {
        Plate = lease.Carplate, IsEV = lease.CarIsEV, Size = lease.IsBig ? VehicleSize.Big : VehicleSize.Small,
        Name = lease.Name, Contact = lease.Contact, Address = lease.Address, LeaseId = lease.Lid
    };
    private static void NotParked(ParkingDbContext db, string plate) =>
        Require(!db.Sessions.Any(s => s.Carplate == plate && s.IsActive),
            ErrorCode.VehicleAlreadyParked, "The vehicle is here; please proceed to checkout.");
    private void RequireAdmin() => Require(_admin, ErrorCode.Unauthorized, "Please log in via the administrator portal first.");

    // Consistently handle business errors; automatically roll back failed transactions. Do not mask unknown programming errors as successes.
    private OperationResult<T> Run<T>(Func<ParkingDbContext, T> action, bool write = false)
    {
        lock (_gate)
        {
            try
            {
                using var db = _factory.CreateDbContext();
                using var tx = write ? db.Database.BeginTransaction() : null;
                var result = action(db);
                if (write) { db.SaveChanges(); tx!.Commit(); }
                return OperationResult<T>.Ok(result);
            }
            catch (BusinessException e) { return OperationResult<T>.Fail(e.Code, e.Message); }
            catch (DbUpdateException) { return OperationResult<T>.Fail(ErrorCode.StorageConflict, "The data status has changed. Please refresh the page and try again. The incomplete write operation has been rolled back."); }
            catch (SqliteException) { return OperationResult<T>.Fail(ErrorCode.StorageConflict, "The database is currently unavailable. Please check the database files or refresh the page and try again."); }
        }
    }

    public OperationResult<Dictionary<string, int>> GetDashboardCounts() => Run(db => new Dictionary<string, int>
    {
        // TotalCount includes parked and unparked spaces; EnabledCount allows the View to display the number of enabled spaces.
        ["TotalCount"] = db.Spaces.Count(),
        ["EnabledCount"] = db.Spaces.Count(s => s.IsActive),
        ["AvailableCount"] = db.Spaces.Count(s => s.IsActive && s.SessionID == -1 && s.LeaseID == -1)
    });

    public OperationResult<ArrivalDecision> ResolveArrival(string input) => Run(db =>
    {
        var raw = (input ?? "").Trim();
        if (raw.StartsWith('@'))
        {
            _admin = false;
            var token = db.Settings.AsNoTracking().SingleOrDefault(s => s.Key == "AdminToken")?.Value;
            Require(token != null && string.Equals(raw, token, StringComparison.Ordinal), ErrorCode.Unauthorized, "The administrator password is incorrect.");
            _admin = true;
            return new ArrivalDecision { Route = ArrivalRoute.Management };
        }
        var plate = Plate(raw);
        // First, check the on-site records to prevent vehicles already on-site from being incorrectly registered again due to changes in the lease agreement.
        var active = db.Sessions.AsNoTracking().SingleOrDefault(s => s.Carplate == plate && s.IsActive);
        if (active != null)
            return new ArrivalDecision { Route = ArrivalRoute.Checkout, Plate = plate, SessionId = active.Sid };
        var lease = db.Leases.AsNoTracking().SingleOrDefault(l => l.Carplate == plate && l.IsActive);
        return lease == null
            ? new ArrivalDecision { Route = ArrivalRoute.MoreInformation, Plate = plate }
            : new ArrivalDecision { Route = ArrivalRoute.TenantSpace, Plate = plate, LeaseId = lease.Lid, Request = TenantRequest(lease) };
    });

    public OperationResult<ParkingRequest> ValidateRegistration(ParkingRequest request) => Run(db =>
    {
        var r = Normalize(request);
        Require(r.LeaseId == null, ErrorCode.InvalidInput, "New registrations cannot use an existing lease ID.");
        NotParked(db, r.Plate);
        Require(!db.Leases.Any(l => l.Carplate == r.Plate && l.IsActive), ErrorCode.LeaseConflict,
            "This vehicle is currently under a valid lease. Please re-enter the license plate number to proceed with the tenant selection process.");
        return r;
    });

    // Available parking spots are filtered only here; the public recommendation service returns only the top recommendation.
    private static List<ParkingSpace> Candidates(ParkingDbContext db, ParkingRequest r, out Lease? lease)
    {
        NotParked(db, r.Plate);
        lease = db.Leases.SingleOrDefault(l => l.Carplate == r.Plate && l.IsActive);
        Require(!r.LeaseId.HasValue || lease?.Lid == r.LeaseId, ErrorCode.LeaseNotValid, "The lease has been canceled or modified. Please re-enter the license plate number.");
        Require(lease == null || r.LeaseId == lease.Lid, ErrorCode.LeaseConflict, "This vehicle is currently under a valid lease. Please re-enter the license plate number.");
        Require(!(r.RegisterLease && lease != null), ErrorCode.LeaseConflict, "There is a valid lease on this vehicle.");
        var spaces = db.Spaces.Where(s => s.IsActive && s.SessionID == -1);
        if (lease != null)
        {
            var lid = lease.Lid;
            var pid = lease.Pid;
            r.IsEV = lease.CarIsEV;
            r.Size = lease.IsBig ? VehicleSize.Big : VehicleSize.Small;
            spaces = spaces.Where(s => s.Pid == pid && s.LeaseID == lid);
        }
        else spaces = spaces.Where(s => s.LeaseID == -1);
        if (r.Size == VehicleSize.Big) spaces = spaces.Where(s => s.IsBig);
        return spaces.OrderBy(s => s.Pid).ToList();
    }
    private static ParkingSpace Selected(ParkingDbContext db, ParkingRequest r, int id, out Lease? lease)
    {
        var candidates = Candidates(db, r, out lease);
        var chosen = candidates.SingleOrDefault(s => s.Pid == id);
        Require(chosen != null, ErrorCode.SpaceUnavailable, "The selected parking space is occupied, rented, out of service, or incompatible with your vehicle's dimensions. Please refresh the page to select another one.");
        return chosen!;
    }
    public OperationResult<SpaceSelectionState> GetSelection(ParkingRequest request, int? selectedId = null) => Run(db =>
    {
        var r = Normalize(request);
        var candidates = Candidates(db, r, out _);
        var ids = candidates.Select(s => s.Pid).ToHashSet();
        var recommended = _recommendations.Recommend(r, candidates);
        var selected = selectedId.HasValue && ids.Contains(selectedId.Value) ? selectedId : recommended?.SpaceId;
        return new SpaceSelectionState
        {
            Recommendation = recommended, SelectedSpaceId = selected,
            Spaces = db.Spaces.AsNoTracking().OrderBy(s => s.MapRow).ThenBy(s => s.MapColumn).ThenBy(s => s.Pid)
                .AsEnumerable().Select(s => new SpaceInfo
                {
                    Id = s.Pid, Code = s.Code, Size = s.IsBig ? VehicleSize.Big : VehicleSize.Small,
                    IsEV = s.IsEV, IsActive = s.IsActive, MapRow = s.MapRow, MapColumn = s.MapColumn,
                    Dest = s.Dest, Status = s.Status, CanSelect = ids.Contains(s.Pid), IsSelected = s.Pid == selected
                }).ToList()
        };
    });

    private ParkingTicket Park(ParkingDbContext db, ParkingRequest r, ParkingSpace space, Lease? lease, long now)
    {
        var rate = space.IsBig ? Constants.DefaultHourlyRateCents * 120 / 100 : Constants.DefaultHourlyRateCents;
        var session = new ParkingSession
        {
            Carplate = r.Plate, Pid = space.Pid, StartTime = now, IsBig = space.IsBig,
            IsLeased = lease != null, LeaseID = lease?.Lid ?? -1, IsActive = true,
            UseEV = false, HourlyRateCents = lease == null ? rate : 0
        };
        db.Sessions.Add(session);
        db.SaveChanges(); // Retrieve the auto-incrementing Sid; still within the outer transaction.
        db.Vehicles.Add(new Vehicle
        {
            Carplate = r.Plate, IsEV = r.IsEV, IsBig = r.Size == VehicleSize.Big,
            LeaseID = lease?.Lid ?? -1, SessionID = session.Sid
        });
        space.SessionID = session.Sid;
        space.LeaseID = lease?.Lid ?? -1;
        RefreshAvailability(space);
        return new ParkingTicket
        {
            SessionId = session.Sid, Plate = r.Plate, SpaceCode = space.Code, EntryUtc = Utc(now),
            FeePolicy = lease == null ? FeePolicyKind.Hourly : FeePolicyKind.Lease
        };
    }
    private static void RefreshAvailability(ParkingSpace s) => s.IsFree = s.IsActive && s.SessionID == -1 && s.LeaseID == -1;

    public OperationResult<ParkingTicket> ConfirmParking(ParkingRequest request, int spaceId) => Run(db =>
    {
        var r = Normalize(request);
        Require(!r.RegisterLease, ErrorCode.InvalidInput, "New monthly subscription customers must first complete their first monthly payment.");
        var space = Selected(db, r, spaceId, out var lease);
        return Park(db, r, space, lease, Now());
    }, write: true);

    public OperationResult<CheckoutQuote> PreviewFirstLease(ParkingRequest request, int spaceId) => Run(db =>
    {
        var r = Normalize(request);
        Require(r.RegisterLease, ErrorCode.InvalidInput, "This is not a new monthly subscription registration.");
        var space = Selected(db, r, spaceId, out _);
        var quote = new CheckoutQuote
        {
            QuoteId = Guid.NewGuid(), Kind = CheckoutKind.FirstLeasePayment, Plate = r.Plate,
            SpaceCode = space.Code, EntryUtc = Utc(Now()), CheckoutUtc = Utc(Now()), AmountCents = MonthlyFee(space)
        };
        // Only one quote is retained for the current window; there are no Lease/Vehicle/Session entries, and no parking spaces are reserved.
        _pending = new PendingCheckout { Quote = quote.Copy(), Request = r.Copy(), SpaceId = space.Pid };
        return quote;
    });
    private CheckoutQuote MakeExitQuote(ParkingSession session, ParkingSpace space, long now)
    {
        var duration = TimeSpan.FromSeconds(Math.Max(0, now - session.StartTime));
        IFeePolicy policy = session.IsLeased ? _leased : _hourly;
        var amount = policy.CalculateCents(duration, session.HourlyRateCents, Constants.DefaultGraceMinutes);
        return new CheckoutQuote
        {
            QuoteId = Guid.NewGuid(), Kind = session.IsLeased ? CheckoutKind.TenantExit : CheckoutKind.TemporaryExit,
            SessionId = session.Sid, Plate = session.Carplate, SpaceCode = space.Code,
            EntryUtc = Utc(session.StartTime), CheckoutUtc = Utc(Math.Max(now, session.StartTime)), Duration = duration,
            HourlyRateCents = session.HourlyRateCents, GraceMinutes = Constants.DefaultGraceMinutes,
            ChargeableHours = session.IsLeased || duration.TotalMinutes <= Constants.DefaultGraceMinutes ? 0 : (long)Math.Ceiling(duration.TotalHours),
            AmountCents = amount
        };
    }
    public OperationResult<CheckoutQuote> PreviewCheckout(int sessionId) => Run(db =>
    {
        var session = db.Sessions.AsNoTracking().SingleOrDefault(s => s.Sid == sessionId);
        Require(session != null, ErrorCode.SessionNotFound, "No parking records found.");
        Require(session!.IsActive, ErrorCode.SessionAlreadyClosed, "Parking is now closed.");
        var space = db.Spaces.Single(s => s.Pid == session.Pid);
        var quote = MakeExitQuote(session, space, Now());
        _pending = new PendingCheckout { Quote = quote.Copy(), SpaceId = space.Pid };
        return quote;
    });

    public OperationResult<CheckoutReceipt> ConfirmCheckout(Guid quoteId)
    {
        lock (_gate)
        {
            var result = ExecuteCheckout(quoteId);
            if (result.Success) _pending = null;
            return result;
        }
    }
    private OperationResult<CheckoutReceipt> ExecuteCheckout(Guid quoteId) => Run(db =>
    {
        Require(_pending != null && _pending.Quote.QuoteId == quoteId, ErrorCode.QuoteExpired,
            "The quote has expired or has been processed. Please request a new quote.");
        var pending = _pending!;
        var now = Now();
        if (pending.Quote.Kind == CheckoutKind.FirstLeasePayment)
        {
            var r = pending.Request!.Copy();
            var space = Selected(db, r, pending.SpaceId, out _);
            var monthly = MonthlyFee(space);
            Require(monthly == pending.Quote.AmountCents, ErrorCode.QuoteChanged, "Parking rates have changed; please refresh the quote.");
            var lease = new Lease
            {
                Carplate = r.Plate, Pid = space.Pid, Name = r.Name, Contact = r.Contact, Address = r.Address,
                CarIsEV = r.IsEV, SpaceIsEV = space.IsEV, IsBig = r.Size == VehicleSize.Big,
                IsActive = true, StartTime = now, EndTime = 0, Pin = 0, UseEV = false,
                FeePaidCents = monthly
            };
            db.Leases.Add(lease);
            db.SaveChanges(); // The Lid has been obtained, but the transaction has not yet been committed.
            var ticket = Park(db, r, space, lease, now);
            // Quote 由 ConfirmCheckout 在事务提交成功后消费。
            return new CheckoutReceipt
            {
                Kind = CheckoutKind.FirstLeasePayment, SessionId = ticket.SessionId, LeaseId = lease.Lid,
                Plate = r.Plate, SpaceCode = space.Code, EntryUtc = ticket.EntryUtc, AmountPaidCents = monthly
            };
        }
        var session = db.Sessions.SingleOrDefault(s => s.Sid == pending.Quote.SessionId);
        Require(session != null, ErrorCode.SessionNotFound, "No parking records found.");
        Require(session!.IsActive, ErrorCode.SessionAlreadyClosed, "Parking has ended; you cannot make another payment.");
        var occupied = db.Spaces.Single(s => s.Pid == session.Pid);
        var current = MakeExitQuote(session, occupied, now);
        // Clicking “Pay” is still a simulated payment; when fees cross a billing threshold, a new quote must be displayed first—prices cannot be increased silently.
        Require(current.AmountCents == pending.Quote.AmountCents, ErrorCode.QuoteChanged,
            "Parking fees have changed. Please refresh the quote and confirm the new amount.");
        Close(db, session, occupied, Math.Max(now, session.StartTime), current.AmountCents, false, "");
        return new CheckoutReceipt
        {
            Kind = current.Kind, SessionId = session.Sid, LeaseId = session.LeaseID == -1 ? null : session.LeaseID,
            Plate = session.Carplate, SpaceCode = occupied.Code, EntryUtc = Utc(session.StartTime),
            ExitUtc = Utc(session.EndTime!.Value), Duration = current.Duration, AmountPaidCents = current.AmountCents
        };
    }, write: true);

    private static void Close(ParkingDbContext db, ParkingSession session, ParkingSpace space,
        long now, long amount, bool adminRemoved, string reason)
    {
        Require(space.SessionID == session.Sid, ErrorCode.StorageConflict, "There is an error in the parking space association status. Please check the data.");
        session.EndTime = now;
        session.IsActive = false;
        session.AmountPaidCents = amount;
        session.AdminRemoved = adminRemoved;
        session.CloseReason = reason;
        var vehicle = db.Vehicles.SingleOrDefault(v => v.Carplate == session.Carplate);
        if (vehicle != null) db.Vehicles.Remove(vehicle);
        space.SessionID = -1;
        RefreshAvailability(space); // Since the LeaseID is retained for regular long-term rentals, this feature is still not available for temporary vehicles.
    }
    public OperationResult<IReadOnlyList<HistoryRecord>> GetHistory() => Run<IReadOnlyList<HistoryRecord>>(db =>
    {
        RequireAdmin();
        var codes = db.Spaces.AsNoTracking().ToDictionary(s => s.Pid, s => s.Code);
        return db.Sessions.AsNoTracking().OrderByDescending(s => s.StartTime).ThenByDescending(s => s.Sid)
            .AsEnumerable().Select(s => new HistoryRecord
            {
                SessionId = s.Sid, Plate = s.Carplate, SpaceCode = codes[s.Pid], EntryUtc = Utc(s.StartTime),
                ExitUtc = s.EndTime.HasValue ? Utc(s.EndTime.Value) : null, IsActive = s.IsActive,
                IsLease = s.IsLeased, AmountPaidCents = s.AmountPaidCents, AdminRemoved = s.AdminRemoved, CloseReason = s.CloseReason
            }).ToList();
    });
    public OperationResult<IReadOnlyList<LeaseInfo>> GetLeases(bool activeOnly = false) => Run<IReadOnlyList<LeaseInfo>>(db =>
    {
        RequireAdmin();
        var codes = db.Spaces.AsNoTracking().ToDictionary(s => s.Pid, s => s.Code);
        return db.Leases.AsNoTracking().Where(l => !activeOnly || l.IsActive)
            .OrderByDescending(l => l.Lid).AsEnumerable().Select(l => new LeaseInfo
            {
                LeaseId = l.Lid, Reference = $"L-{l.Lid:0000}", Plate = l.Carplate,
                Name = l.Name, Contact = l.Contact, Address = l.Address,
                VehicleSize = l.IsBig ? VehicleSize.Big : VehicleSize.Small, CarIsEV = l.CarIsEV, SpaceIsEV = l.SpaceIsEV,
                SpaceId = l.Pid, SpaceCode = codes[l.Pid], StartUtc = Utc(l.StartTime), IsActive = l.IsActive,
                CancelledUtc = l.CancelledTime.HasValue ? Utc(l.CancelledTime.Value) : null, FeePaidCents = l.FeePaidCents
            }).ToList();
    });
    public OperationResult CancelLease(int leaseId) => Run(db =>
    {
        RequireAdmin();
        var lease = db.Leases.SingleOrDefault(l => l.Lid == leaseId);
        Require(lease != null, ErrorCode.LeaseNotFound, "Can't find the lease.");
        if (!lease!.IsActive) return true; // Repeated cancellation is a side-effect-free success.
        var now = Now();
        var space = db.Spaces.Single(s => s.Pid == lease.Pid);
        var session = db.Sessions.SingleOrDefault(s => s.LeaseID == leaseId && s.IsActive);
        if (session != null)
            Close(db, session, space, Math.Max(now, session.StartTime), 0, true, "The administrator cancels the lease, and the vehicle is released free of charge at the same time.");
        lease.IsActive = false;
        lease.CancelledTime = now;
        if (space.LeaseID == leaseId) space.LeaseID = -1;
        RefreshAvailability(space);
        return true;
    }, write: true);
}
