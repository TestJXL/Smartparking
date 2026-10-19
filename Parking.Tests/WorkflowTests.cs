using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Parking.Application;
using Parking.Application.Persistence;
using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;

namespace Parking.Tests;

public sealed class FakeClock : IClock
{
    public DateTime UtcNow { get; set; } = new(2026, 10, 17, 0, 0, 0, DateTimeKind.Utc);
    public void Advance(int minutes) => UtcNow = UtcNow.AddMinutes(minutes);
}
public sealed class FakeShell : IShellView
{
    public ScreenId Screen { get; private set; }
    public void ShowScreen(ScreenId screen) => Screen = screen;
}
[TestFixture]
public class WorkflowTests
{
    private string _directory = null!;
    private EfParkingStore _store = null!;
    private ParkingApplicationService _app = null!;
    private FakeClock _clock = null!;
    private FakeShell _shell = null!;
    private ParkingControllers _ui = null!;
    [SetUp]
    public void Setup()
    {
        _directory = Path.Combine(Path.GetTempPath(), "smartpark-tests-" + Guid.NewGuid());
        _store = new EfParkingStore(Path.Combine(_directory, "test.db"));
        _store.Initialize();
        _clock = new FakeClock();
        _app = new ParkingApplicationService(_store, _clock);
        _shell = new FakeShell();
        _ui = new ParkingControllers(_app, _shell);
    }
    [TearDown]
    public void Cleanup()
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        Directory.Delete(_directory, true);
    }
    private static T Ok<T>(OperationResult<T> result)
    {
        Assert.That(result.Success, Is.True, result.Message);
        return result.Value!;
    }
    private static ParkingRequest Request(string plate = "ABC123", bool lease = false, bool big = false, bool ev = false) => new()
    {
        Plate = plate, RegisterLease = lease, Size = big ? VehicleSize.Big : VehicleSize.Small,
        IsEV = ev, Name = "Alice", Contact = "alice@example.test", Address = "1 Example Road"
    };
    private CheckoutReceipt StartLease(string plate = "ABC123", int space = 1)
    {
        var quote = Ok(_app.PreviewFirstLease(Request(plate, lease: true), space));
        return Ok(_app.ConfirmCheckout(quote.QuoteId));
    }
    private void Admin() => Ok(_app.ResolveArrival("@admin"));

    [TestCase(0, 0)]
    [TestCase(10, 0)]
    [TestCase(11, 500)]
    [TestCase(60, 500)]
    [TestCase(61, 1000)]
    public void HourlyFee_Boundaries(int minutes, long expected)
    {
        IFeePolicy policy = new HourlyFeePolicy();
        Assert.That(policy.CalculateCents(TimeSpan.FromMinutes(minutes), 500, 10), Is.EqualTo(expected));
    }
    [Test]
    public void NegativeFeeInput_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HourlyFeePolicy().CalculateCents(TimeSpan.FromSeconds(-1), 500, 10));
    }
    [Test]
    public void TemporaryFlow_UsesControllers_AndPersistsHistory()
    {
        _ui.Dashboard.OpenArrival();
        Assert.That(_shell.Screen, Is.EqualTo(ScreenId.Registration));
        Assert.That(Ok(_ui.Arrival.SubmitPlate("ab-c 123")).Route, Is.EqualTo(ArrivalRoute.MoreInformation));
        Ok(_ui.Arrival.SubmitDetails(Request()));
        var grid = Ok(_ui.SpaceSelection.Load());
        Assert.That(grid.SelectedSpaceId, Is.EqualTo(1));
        Assert.That(grid.Spaces.Count(s => s.Color == SpaceCellColor.Green), Is.EqualTo(1));
        var ticket = Ok(_ui.SpaceSelection.Next()).Ticket!;
        _clock.Advance(61);
        Ok(_ui.Arrival.SubmitPlate("abc123"));
        Assert.That(_shell.Screen, Is.EqualTo(ScreenId.Checkout));
        var quote = Ok(_ui.Checkout.Load());
        Assert.That(quote.AmountCents, Is.EqualTo(1000));
        using (var db = _store.CreateDbContext()) Assert.That(db.Sessions.Single().IsActive, Is.True);
        var receipt = Ok(_ui.Checkout.Confirm());
        Assert.That(receipt.Duration, Is.EqualTo(TimeSpan.FromMinutes(61)));
        Assert.That(_ui.Checkout.Confirm().Success, Is.False);
        using var check = _store.CreateDbContext();
        Assert.Multiple(() =>
        {
            Assert.That(check.Vehicles.Count(), Is.Zero);
            Assert.That(check.Sessions.Single().Sid, Is.EqualTo(ticket.SessionId));
            Assert.That(check.Sessions.Single().AmountPaidCents, Is.EqualTo(1000));
            Assert.That(check.Spaces.Single(s => s.Pid == 1).IsFree, Is.True);
        });
    }
    [Test]
    public void BigSpace_CostsTwentyPercentMore_EvenForSmallCar()
    {
        var ticket = Ok(_app.ConfirmParking(Request(), 3));
        _clock.Advance(11);
        Assert.That(Ok(_app.PreviewCheckout(ticket.SessionId)).AmountCents, Is.EqualTo(600));
    }
    [Test]
    public void BigCar_CannotUseSmallSpace_ButEvMismatchIsAllowed()
    {
        Assert.That(_app.ConfirmParking(Request(big: true), 1).Success, Is.False);
        Assert.That(_app.ConfirmParking(Request(big: true, ev: true), 3).Success, Is.True);
    }
    [Test]
    public void Recommendation_MatchesBeforeDistance_AndOnlyOneIsSelected()
    {
        using (var db = _store.CreateDbContext())
        {
            db.Spaces.Single(s => s.Pid == 2).Dest = 100;
            db.Spaces.Single(s => s.Pid == 6).Dest = 200;
            db.Spaces.Single(s => s.Pid == 10).Dest = 300;
            db.SaveChanges();
        }
        var result = Ok(_app.GetSelection(Request(ev: true)));
        Assert.That(result.Recommendation!.SpaceId, Is.EqualTo(2));
        Assert.That(result.Spaces.Count(s => s.CanSelect), Is.EqualTo(12));
        Assert.That(Ok(_app.GetSelection(Request(ev: true), 1)).SelectedSpaceId, Is.EqualTo(1));
    }
    [Test]
    public void NoCompatibleSpace_ReturnsEmptyRecommendationAndNoSelection()
    {
        using (var db = _store.CreateDbContext())
        {
            foreach (var s in db.Spaces) { s.IsActive = false; s.IsFree = false; }
            db.SaveChanges();
        }
        var result = Ok(_app.GetSelection(Request()));
        Assert.That(result.Recommendation, Is.Null);
        Assert.That(result.SelectedSpaceId, Is.Null);
        Assert.That(result.Spaces.All(s => s.Color == SpaceCellColor.Red), Is.True);
    }
    [Test]
    public void AbandonRegistrationAndLeasePayment_DoesNotWriteOrReserve()
    {
        Ok(_ui.Arrival.SubmitPlate("ABC123"));
        Ok(_ui.Arrival.SubmitDetails(Request(lease: true)));
        Ok(_ui.SpaceSelection.Load());
        Ok(_ui.SpaceSelection.Next());
        _ui.Checkout.Cancel();
        using var db = _store.CreateDbContext();
        Assert.That(db.Vehicles.Count() + db.Sessions.Count() + db.Leases.Count(), Is.Zero);
        Assert.That(db.Spaces.All(s => s.SessionID == -1 && s.LeaseID == -1), Is.True);
    }
    [Test]
    public void FirstLease_LargeSpace240_ThenRepeatedEntryAndExitAreFree()
    {
        var receipt = StartLease(space: 3);
        Assert.That(receipt.AmountPaidCents, Is.EqualTo(24000));
        Assert.That(receipt.ExitUtc, Is.Null);
        _clock.Advance(100);
        var quote = Ok(_app.PreviewCheckout(receipt.SessionId));
        Assert.That(quote.AmountCents, Is.Zero);
        Ok(_app.ConfirmCheckout(quote.QuoteId));
        using (var db = _store.CreateDbContext())
        {
            Assert.That(db.Vehicles.Count(), Is.Zero);
            Assert.That(db.Spaces.Single(s => s.Pid == 3).IsFree, Is.False);
        }
        // 租约无限期，远期也可再次入场。
        _clock.UtcNow = _clock.UtcNow.AddYears(5);
        var arrival = Ok(_app.ResolveArrival("ABC123"));
        var choices = Ok(_app.GetSelection(arrival.Request!));
        Assert.That(choices.Spaces.Where(s => s.CanSelect).Select(s => s.Id), Is.EqualTo(new[] { 3 }));
        Ok(_app.ConfirmParking(arrival.Request!, 3));
    }
    [Test]
    public void LeaseCancel_ClosesParkedCarWithoutFee_AndPreservesHistory()
    {
        var receipt = StartLease();
        Assert.That(receipt.AmountPaidCents, Is.EqualTo(20000));
        _clock.Advance(123);
        Admin();
        Assert.That(_app.CancelLease(receipt.LeaseId!.Value).Success, Is.True);
        Assert.That(_app.CancelLease(receipt.LeaseId.Value).Success, Is.True);
        using var db = _store.CreateDbContext();
        var session = db.Sessions.Single();
        Assert.Multiple(() =>
        {
            Assert.That(session.IsActive, Is.False);
            Assert.That(session.AdminRemoved, Is.True);
            Assert.That(session.AmountPaidCents, Is.Zero);
            Assert.That(session.EndTime - session.StartTime, Is.EqualTo(123 * 60));
            Assert.That(db.Vehicles.Count(), Is.Zero);
            Assert.That(db.Leases.Single().IsActive, Is.False);
            Assert.That(db.Leases.Single().CancelledTime, Is.Not.Null);
            Assert.That(db.Spaces.Single(s => s.Pid == 1).IsFree, Is.True);
            Assert.That(Ok(_app.GetLeases(true)), Is.Empty);
            Assert.That(Ok(_app.GetHistory()).Count, Is.EqualTo(1));
        });
    }
    [Test]
    public void CancelLeaseWhenVehicleAlreadyAway_ReleasesSpace()
    {
        var receipt = StartLease();
        Ok(_app.ConfirmCheckout(Ok(_app.PreviewCheckout(receipt.SessionId)).QuoteId));
        Admin();
        Assert.That(_app.CancelLease(receipt.LeaseId!.Value).Success, Is.True);
        Assert.That(Ok(_app.ResolveArrival("ABC123")).Route, Is.EqualTo(ArrivalRoute.MoreInformation));
        Assert.That(_app.ConfirmParking(Request("OTHER"), 1).Success, Is.True);
    }
    [Test]
    public void LeaseCancelledDuringSelection_CannotBecomeTemporaryEntrySilently()
    {
        var receipt = StartLease();
        Ok(_app.ConfirmCheckout(Ok(_app.PreviewCheckout(receipt.SessionId)).QuoteId));
        var request = Ok(_app.ResolveArrival("ABC123")).Request!;
        Admin();
        _app.CancelLease(receipt.LeaseId!.Value);
        Assert.That(_app.ConfirmParking(request, 1).Error, Is.EqualTo(ErrorCode.LeaseNotValid));
    }
    [Test]
    public void StaleSpaceChoice_IsRecheckedAtPayment()
    {
        var quote = Ok(_app.PreviewFirstLease(Request(lease: true), 1));
        var other = new ParkingApplicationService(_store, _clock);
        Ok(other.ConfirmParking(Request("OTHER"), 1));
        Assert.That(_app.ConfirmCheckout(quote.QuoteId).Success, Is.False);
        using var db = _store.CreateDbContext();
        Assert.That(db.Leases.Count(), Is.Zero);
        Assert.That(db.Vehicles.Single().Carplate, Is.EqualTo("OTHER"));
    }
    [Test]
    public void RepeatedPaymentAndVehicleEntry_DoNotDuplicateRows()
    {
        var quote = Ok(_app.PreviewFirstLease(Request(lease: true), 1));
        Ok(_app.ConfirmCheckout(quote.QuoteId));
        Assert.That(_app.ConfirmCheckout(quote.QuoteId).Error, Is.EqualTo(ErrorCode.QuoteExpired));
        Assert.That(_app.ConfirmParking(Request(), 2).Success, Is.False);
        using var db = _store.CreateDbContext();
        Assert.That(db.Sessions.Count(), Is.EqualTo(1));
        Assert.That(db.Leases.Count(), Is.EqualTo(1));
    }
    [Test]
    public void QuoteCannotBeTampered_AndBoundaryRequiresFreshConfirmation()
    {
        var ticket = Ok(_app.ConfirmParking(Request(), 1));
        _clock.Advance(59);
        var quote = Ok(_app.PreviewCheckout(ticket.SessionId));
        quote.AmountCents = 0; // View 的副本，不会更改服务保存的报价。
        _clock.Advance(2);
        Assert.That(_app.ConfirmCheckout(quote.QuoteId).Error, Is.EqualTo(ErrorCode.QuoteChanged));
        using (var db = _store.CreateDbContext()) Assert.That(db.Sessions.Single().IsActive, Is.True);
        var fresh = Ok(_app.PreviewCheckout(ticket.SessionId));
        fresh.AmountCents = 1;
        Assert.That(Ok(_app.ConfirmCheckout(fresh.QuoteId)).AmountPaidCents, Is.EqualTo(1000));
    }
    [Test]
    public void AdminToken_IsExactFromDatabase_AndProtectedOperationsRequireLogin()
    {
        Assert.That(_app.GetHistory().Error, Is.EqualTo(ErrorCode.Unauthorized));
        Assert.That(_app.GetLeases().Error, Is.EqualTo(ErrorCode.Unauthorized));
        Assert.That(_app.CancelLease(1).Error, Is.EqualTo(ErrorCode.Unauthorized));
        using (var db = _store.CreateDbContext())
        {
            db.Settings.Single().Value = "@staff42";
            db.SaveChanges();
        }
        Assert.That(_app.ResolveArrival("@admin").Success, Is.False);
        Assert.That(_app.ResolveArrival("@anything").Success, Is.False);
        Ok(_app.ResolveArrival("@staff42"));
        Assert.That(_app.GetHistory().Success, Is.True);
        _app.LogoutAdministrator();
        Assert.That(_app.GetHistory().Success, Is.False);
    }
    [Test]
    public void LeaseAndSessionInsert_RollBackTogether_IfVehicleWriteFails()
    {
        using (var db = _store.CreateDbContext())
            db.Database.ExecuteSqlRaw("CREATE TRIGGER fail_vehicle BEFORE INSERT ON Vehicles BEGIN SELECT RAISE(ABORT, 'test failure'); END;");
        var quote = Ok(_app.PreviewFirstLease(Request(lease: true), 1));
        Assert.That(_app.ConfirmCheckout(quote.QuoteId).Success, Is.False);
        using (var db = _store.CreateDbContext())
        {
            Assert.That(db.Leases.Count() + db.Sessions.Count() + db.Vehicles.Count(), Is.Zero);
            Assert.That(db.Spaces.Single(s => s.Pid == 1).IsFree, Is.True);
            db.Database.ExecuteSqlRaw("DROP TRIGGER fail_vehicle;");
        }
        // 事务失败没有消费报价，可以重试。
        Assert.That(_app.ConfirmCheckout(quote.QuoteId).Success, Is.True);
    }
    [Test]
    public void NewStoreAndService_ReadPersistedParkingAfterRestart()
    {
        Ok(_app.ConfirmParking(Request(), 1));
        var reopened = new EfParkingStore(Path.Combine(_directory, "test.db"));
        reopened.Initialize();
        var service = new ParkingApplicationService(reopened, _clock);
        Assert.That(Ok(service.ResolveArrival("ABC123")).Route, Is.EqualTo(ArrivalRoute.Checkout));
        Assert.That(Ok(service.GetDashboardCounts())["AvailableCount"], Is.EqualTo(11));
    }
    [Test]
    public void TwoWindowsCannotCommitSameSpace()
    {
        var other = new ParkingApplicationService(_store, _clock);
        var results = Task.WhenAll(
            Task.Run(() => _app.ConfirmParking(Request("FIRST"), 1)),
            Task.Run(() => other.ConfirmParking(Request("SECOND"), 1))).GetAwaiter().GetResult();
        Assert.That(results.Count(r => r.Success), Is.EqualTo(1));
        using var db = _store.CreateDbContext();
        Assert.That(db.Sessions.Count(), Is.EqualTo(1));
        Assert.That(db.Vehicles.Count(), Is.EqualTo(1));
    }
}
