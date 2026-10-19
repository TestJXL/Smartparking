using Parking.Core.DTOs;
using Parking.Core.Interfaces;
namespace Parking.Controllers;
public sealed class HistoryController
{
    private readonly IParkingApplicationService _service;
    internal HistoryController(IParkingApplicationService service) => _service = service;
    public OperationResult<IReadOnlyList<HistoryRecord>> Load() => _service.GetHistory();
}
