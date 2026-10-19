using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
namespace Parking.Controllers;
public sealed class LeaseCancellationController
{
    private readonly IParkingApplicationService _service;
    internal LeaseCancellationController(IParkingApplicationService service) => _service = service;
    public OperationResult<IReadOnlyList<LeaseInfo>> Load() => _service.GetLeases(activeOnly: true);
    public OperationResult<LeaseInfo> Select(int leaseId)
    {
        var result = Load();
        if (!result.Success) return OperationResult<LeaseInfo>.Fail(result.Error!.Value, result.Message);
        var lease = result.Value!.SingleOrDefault(l => l.LeaseId == leaseId);
        return lease == null ? OperationResult<LeaseInfo>.Fail(ErrorCode.LeaseNotFound, "The lease does not exist or has been cancelled; please refresh the list.")
            : OperationResult<LeaseInfo>.Ok(lease);
    }
    // The View should pass the LeaseId of the selected item in the ListBox; it should not pass the display index.
    public OperationResult Cancel(int leaseId) => _service.CancelLease(leaseId);
}
