using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;
namespace Parking.Controllers;

public sealed class ManagementController
{
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    internal ManagementController(IParkingApplicationService service, IShellView shell) => (_service, _shell) = (service, shell);
    public OperationResult Load() => _service.IsAdministrator ? OperationResult.Ok()
        : OperationResult.Fail(ErrorCode.Unauthorized, "Please enter the administrator password first.");
    public OperationResult Open(ScreenId screen)
    {
        var access = Load();
        if (!access.Success) return access;
        if (screen is not (ScreenId.Management or ScreenId.History or ScreenId.LeaseList or ScreenId.LeaseCancellation))
            return OperationResult.Fail(ErrorCode.InvalidInput, "Not an administrator subpage.");
        _shell.ShowScreen(screen);
        return OperationResult.Ok();
    }
    public void Logout()
    {
        _service.LogoutAdministrator();
        _shell.ShowScreen(ScreenId.Dashboard);
    }
}
