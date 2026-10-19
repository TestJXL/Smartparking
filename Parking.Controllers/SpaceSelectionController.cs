using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;
namespace Parking.Controllers;

public sealed class SpaceSelectionController
{
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    private readonly FlowState _flow;
    internal SpaceSelectionController(IParkingApplicationService service, IShellView shell, FlowState flow)
        => (_service, _shell, _flow) = (service, shell, flow);

    public OperationResult<SpaceSelectionState> Load()
    {
        if (_flow.Request == null)
            return OperationResult<SpaceSelectionState>.Fail(ErrorCode.InvalidInput, "Please complete the vehicle information entry first.");
        var result = _service.GetSelection(_flow.Request, _flow.SelectedSpaceId);
        if (result.Success) _flow.SelectedSpaceId = result.Value!.SelectedSpaceId;
        else _flow.SelectedSpaceId = null;
        return result;
    }
    public OperationResult<SpaceSelectionState> Select(int spaceId)
    {
        if (_flow.Request == null)
            return OperationResult<SpaceSelectionState>.Fail(ErrorCode.InvalidInput, "Registration information is missing.");
        var result = _service.GetSelection(_flow.Request, spaceId);
        if (!result.Success) return result;
        if (!result.Value!.Spaces.Any(s => s.Id == spaceId && s.CanSelect))
            return OperationResult<SpaceSelectionState>.Fail(ErrorCode.SpaceUnavailable, "This parking space cannot be selected; please refresh the grid.");
        _flow.SelectedSpaceId = spaceId;
        return result; // Return the entire grid, ensuring there is only one green cell.
    }
    public OperationResult<SelectionOutcome> Next()
    {
        if (_flow.Request == null || !_flow.SelectedSpaceId.HasValue)
            return OperationResult<SelectionOutcome>.Fail(ErrorCode.NoSuitableSpace, "Please select an available parking space first.");
        if (_flow.Request.RegisterLease)
        {
            var quote = _service.PreviewFirstLease(_flow.Request, _flow.SelectedSpaceId.Value);
            if (!quote.Success) return OperationResult<SelectionOutcome>.Fail(quote.Error!.Value, quote.Message);
            _flow.QuoteId = quote.Value!.QuoteId;
            _shell.ShowScreen(ScreenId.Checkout);
            return OperationResult<SelectionOutcome>.Ok(new SelectionOutcome { Quote = quote.Value });
        }
        var ticket = _service.ConfirmParking(_flow.Request, _flow.SelectedSpaceId.Value);
        if (!ticket.Success) return OperationResult<SelectionOutcome>.Fail(ticket.Error!.Value, ticket.Message);
        _flow.Reset(_service);
        _shell.ShowScreen(ScreenId.Dashboard);
        return OperationResult<SelectionOutcome>.Ok(new SelectionOutcome { Ticket = ticket.Value });
    }
}
