using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;
namespace Parking.Controllers;

public sealed class CheckoutController
{
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    private readonly FlowState _flow;
    internal CheckoutController(IParkingApplicationService service, IShellView shell, FlowState flow)
        => (_service, _shell, _flow) = (service, shell, flow);

    // Called when the page loads or the user refreshes it. Refreshing replaces the old quote.
    public OperationResult<CheckoutQuote> Load()
    {
        OperationResult<CheckoutQuote> result;
        if (_flow.ExitSessionId.HasValue)
            result = _service.PreviewCheckout(_flow.ExitSessionId.Value);
        else if (_flow.Request?.RegisterLease == true && _flow.SelectedSpaceId.HasValue)
            result = _service.PreviewFirstLease(_flow.Request, _flow.SelectedSpaceId.Value);
        else return OperationResult<CheckoutQuote>.Fail(ErrorCode.InvalidInput, "There are no pending settlement processes.");
        if (result.Success) _flow.QuoteId = result.Value!.QuoteId;
        return result;
    }
    // "Pay and Exit," "Confirm Exit," and "Pay Monthly Fee and Enter" all use this entry point.
    public OperationResult<CheckoutReceipt> Confirm()
    {
        if (!_flow.QuoteId.HasValue)
            return OperationResult<CheckoutReceipt>.Fail(ErrorCode.InvalidInput, "Please load the quote first, or note that the current settlement has already been completed.");
        var result = _service.ConfirmCheckout(_flow.QuoteId.Value);
        if (result.Success) _flow.Reset(_service);
        // Even if the transaction is successful, the page remains on the settlement page: The view displays the refund receipt, then calls `Done()`.
        // When `QuoteChanged` occurs, the view calls `Load()` to display the new price; the user must click "Confirm" again.
        return result;
    }
    public void Done()
    {
        _flow.Reset(_service);
        _shell.ShowScreen(ScreenId.Dashboard);
    }
    public void Cancel() => Done(); // Discard drafts only; on-site parking remains unaffected.
}
