using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;
namespace Parking.Controllers;

public sealed class ArrivalController
{
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    private readonly FlowState _flow;
    internal ArrivalController(IParkingApplicationService service, IShellView shell, FlowState flow)
        => (_service, _shell, _flow) = (service, shell, flow);

    public OperationResult<ArrivalDecision> SubmitPlate(string input)
    {
        _flow.Reset(_service);
        var result = _service.ResolveArrival(input);
        if (!result.Success) return result;
        var decision = result.Value!;
        _flow.Plate = decision.Plate;
        switch (decision.Route)
        {
            case ArrivalRoute.Management:
                _shell.ShowScreen(ScreenId.Management);
                break;
            case ArrivalRoute.Checkout:
                _flow.ExitSessionId = decision.SessionId;
                _shell.ShowScreen(ScreenId.Checkout);
                break;
            case ArrivalRoute.TenantSpace:
                _flow.Request = decision.Request!.Copy();
                _shell.ShowScreen(ScreenId.SpaceMap);
                break;
            // MoreInformation Remains on the current page; the View expands the vehicle information input area.
        }
        return result;
    }
    public OperationResult<ParkingRequest> SubmitDetails(ParkingRequest request)
    {
        if (_flow.Plate == null || _flow.ExitSessionId.HasValue || _flow.Request != null)
            return OperationResult<ParkingRequest>.Fail(ErrorCode.InvalidInput, "Please submit your license plate number first to proceed to the additional information stage.");
        if (request == null)
            return OperationResult<ParkingRequest>.Fail(ErrorCode.InvalidInput, "Registration information is missing.");
        var draft = request.Copy();
        draft.Plate = _flow.Plate;
        var result = _service.ValidateRegistration(draft);
        if (!result.Success) return result;
        _flow.Request = result.Value!.Copy();
        _shell.ShowScreen(ScreenId.SpaceMap);
        return result;
    }
}
