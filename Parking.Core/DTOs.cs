using Parking.Core.Enums;

namespace Parking.Core.DTOs;

// The View interacts only with DTOs and does not directly modify EF entities. Amount: Australian cents; Time: UTC.
public class OperationResult
{
    public bool Success { get; set; }
    public ErrorCode? Error { get; set; }
    public string Message { get; set; } = "";
    public static OperationResult Ok() => new() { Success = true };
    public static OperationResult Fail(ErrorCode error, string message) => new() { Error = error, Message = message };
}
public class OperationResult<T> : OperationResult
{
    public T? Value { get; set; }
    public static OperationResult<T> Ok(T value) => new() { Success = true, Value = value };
    public new static OperationResult<T> Fail(ErrorCode error, string message) => new() { Error = error, Message = message };
}
public class ParkingRequest
{
    public string Plate { get; set; } = "";
    public VehicleSize Size { get; set; }
    public bool IsEV { get; set; }
    public bool RegisterLease { get; set; }
    // Existing tenants are entered via ResolveArrival to prevent accidental entry of temporary vehicles if a lease is canceled during the space selection process.
    public int? LeaseId { get; set; }
    public string Name { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Address { get; set; } = "";
    // The original field is retained to facilitate integration; this version sorts only by ParkingSpace.Dest and does not query the destination table.
    public int DestinationId { get; set; }
    public ParkingRequest Copy() => (ParkingRequest)MemberwiseClone();
}
public class ArrivalDecision
{
    public ArrivalRoute Route { get; set; }
    public string Plate { get; set; } = "";
    public int? SessionId { get; set; }
    public ParkingRequest? Request { get; set; }
    public int? LeaseId { get; set; }
}
public class SpaceInfo
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public VehicleSize Size { get; set; }
    public bool IsEV { get; set; }
    public int MapRow { get; set; }
    public int MapColumn { get; set; }
    public int Dest { get; set; }
    public SpaceStatus Status { get; set; }
    public bool IsActive { get; set; }
    // Unlike IsFree: Active tenants can also select their own vacant parking spaces.
    public bool CanSelect { get; set; }
    public bool IsSelected { get; set; }
    public SpaceCellColor Color => !CanSelect ? SpaceCellColor.Red : IsSelected ? SpaceCellColor.Green : SpaceCellColor.White;
}
public class SpaceRecommendation
{
    public int SpaceId { get; set; }
    public string Code { get; set; } = "";
    public int DistanceMetres { get; set; }
    public string Reason { get; set; } = "";
}
public class SpaceSelectionState
{
    public IReadOnlyList<SpaceInfo> Spaces { get; set; } = Array.Empty<SpaceInfo>();
    public SpaceRecommendation? Recommendation { get; set; } // null if no parking spaces are available
    public int? SelectedSpaceId { get; set; }
}
public class ParkingTicket
{
    public int SessionId { get; set; }
    public string Plate { get; set; } = "";
    public string SpaceCode { get; set; } = "";
    public DateTime EntryUtc { get; set; }
    public FeePolicyKind FeePolicy { get; set; }
}
public class LeaseInfo
{
    public int LeaseId { get; set; }
    public string Reference { get; set; } = "";
    public string Plate { get; set; } = "";
    public string Name { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Address { get; set; } = "";
    public VehicleSize VehicleSize { get; set; }
    public bool CarIsEV { get; set; }
    public bool SpaceIsEV { get; set; }
    public int SpaceId { get; set; }
    public string SpaceCode { get; set; } = "";
    public DateTime StartUtc { get; set; }
    public DateTime? CancelledUtc { get; set; }
    public bool IsActive { get; set; }
    public long FeePaidCents { get; set; }
    
}
public class CheckoutQuote
{
    // Submit only this ID during confirmation; do not trust the amount or vehicle information returned by the View.
    public Guid QuoteId { get; set; }
    public CheckoutKind Kind { get; set; }
    public int? SessionId { get; set; }
    public string Plate { get; set; } = "";
    public string SpaceCode { get; set; } = "";
    public DateTime EntryUtc { get; set; }
    public DateTime CheckoutUtc { get; set; }
    public TimeSpan Duration { get; set; }
    public long ChargeableHours { get; set; }
    public long HourlyRateCents { get; set; }
    public int GraceMinutes { get; set; }
    public long AmountCents { get; set; }
    public CheckoutQuote Copy() => (CheckoutQuote)MemberwiseClone();
}
public class CheckoutReceipt
{
    public CheckoutKind Kind { get; set; }
    public int SessionId { get; set; }
    public int? LeaseId { get; set; }
    public string Plate { get; set; } = "";
    public string SpaceCode { get; set; } = "";
    public DateTime EntryUtc { get; set; }
    // The first monthly rent payment does not constitute an exit; ExitUtc is null.
    public DateTime? ExitUtc { get; set; }
    public TimeSpan Duration { get; set; }
    public long AmountPaidCents { get; set; }
}
public class HistoryRecord
{
    public int SessionId { get; set; }
    public string Plate { get; set; } = "";
    public string SpaceCode { get; set; } = "";
    public DateTime EntryUtc { get; set; }
    public DateTime? ExitUtc { get; set; }
    public bool IsActive { get; set; }
    public bool IsLease { get; set; }
    public long AmountPaidCents { get; set; }
    public bool AdminRemoved { get; set; }
    public string CloseReason { get; set; } = "";
}
