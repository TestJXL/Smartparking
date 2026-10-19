using Parking.Core.DTOs;
using Parking.Core.Interfaces;
namespace Parking.Controllers;
public sealed class LeaseListController
{
    private readonly IParkingApplicationService _service;
    internal LeaseListController(IParkingApplicationService service) => _service = service;
    public OperationResult<IReadOnlyList<LeaseInfo>> Load() => _service.GetLeases();
}
