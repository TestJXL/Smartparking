using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Views;

namespace Parking.Controllers;

// Each Shell creates a set. All Controllers share the same flow state and business services.
// Page events directly call exposed methods; results are bound by the View and do not depend on WinForms or WPF.
public sealed class ParkingControllers
{
    private readonly FlowState _flow;
    private readonly IParkingApplicationService _service;
    private readonly IShellView _shell;
    public DashboardController Dashboard { get; }
    public ArrivalController Arrival { get; }
    public SpaceSelectionController SpaceSelection { get; }
    public CheckoutController Checkout { get; }
    public ManagementController Management { get; }
    public HistoryController History { get; }
    public LeaseListController LeaseList { get; }
    public LeaseCancellationController LeaseCancellation { get; }

    public ParkingControllers(IParkingApplicationService service, IShellView shell)
    {
        _service = service;
        _shell = shell;
        _flow = new FlowState();
        Dashboard = new(service, shell, _flow);
        Arrival = new(service, shell, _flow);
        SpaceSelection = new(service, shell, _flow);
        Checkout = new(service, shell, _flow);
        Management = new(service, shell);
        History = new(service);
        LeaseList = new(service);
        LeaseCancellation = new(service);
    }
    // "Cancel" and "Return to Home": These actions only discard unsaved screen data; they do not end the current parking session.
    public void Home()
    {
        _flow.Reset(_service);
        _service.LogoutAdministrator();
        _shell.ShowScreen(ScreenId.Dashboard);
    }
}

internal sealed class FlowState
{
    public string? Plate { get; set; }
    public ParkingRequest? Request { get; set; }
    public int? SelectedSpaceId { get; set; }
    public int? ExitSessionId { get; set; }
    public Guid? QuoteId { get; set; }
    public void Reset(IParkingApplicationService service)
    {
        if (QuoteId.HasValue) service.DiscardQuote(QuoteId.Value);
        Plate = null; Request = null; SelectedSpaceId = null; ExitSessionId = null; QuoteId = null;
    }
}

public class SelectionOutcome
{
    public ParkingTicket? Ticket { get; set; }
    public CheckoutQuote? Quote { get; set; }
}
