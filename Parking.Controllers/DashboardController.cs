using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;
namespace Parking.Controllers;

public sealed class DashboardController
{
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    private readonly FlowState _flow;
    internal DashboardController(IParkingApplicationService service, IShellView shell, FlowState flow)
        => (_service, _shell, _flow) = (service, shell, flow);
    // `Value` is a dictionary whose keys are `AvailableCount`, `TotalCount`, and `EnabledCount`.
    public OperationResult<Dictionary<string, int>> Load() => _service.GetDashboardCounts();
    public void OpenArrival()
    {
        _flow.Reset(_service);
        _service.LogoutAdministrator();
        _shell.ShowScreen(ScreenId.Registration);
    }
}
