using Parking.Controllers;
using Parking.Core.Enums;
using Parking.Core.Views;
using Parking.WinForms.Screens;

namespace Parking.WinForms
{
    public partial class MainForm : Form, IShellView
    {
        private readonly DashboardScreen dashboardScreen = new();
        private readonly PlateEntryScreen plateEntryScreen = new();
        private readonly SpaceMapScreen spaceMapScreen = new();
        private readonly CheckoutScreen checkoutScreen = new();
        private readonly AdminDashboardScreen adminScreen = new();
        private readonly ParkingControllers controllers;

        // The controller requires the main window as its IShellView, so the main window passes itself to the factory to create the controller
        public MainForm(Func<IShellView, ParkingControllers> createControllers)
        {
            InitializeComponent();
            controllers = createControllers(this);

            dashboardScreen.Bind(controllers);
            plateEntryScreen.Bind(controllers);
            spaceMapScreen.Bind(controllers);
            checkoutScreen.Bind(controllers);
            adminScreen.Bind(controllers);

            foreach (Control screen in new Control[] { dashboardScreen, plateEntryScreen, spaceMapScreen, checkoutScreen, adminScreen })
            {
                screen.Dock = DockStyle.Fill;
                screen.Visible = false;
                contentPanel.Controls.Add(screen);
            }
        }

        public void ShowScreen(ScreenId screen)
        {
            // The four ScreenIDs in the “Management” category share the same administrator page; they simply switch between different tabs.
            Control target;
            string title;
            switch (screen)
            {
                case ScreenId.Dashboard:
                    (target, title) = (dashboardScreen, "Home");
                    break;
                case ScreenId.Registration:
                    (target, title) = (plateEntryScreen, "Vehicle Gate");
                    break;
                case ScreenId.SpaceMap:
                    (target, title) = (spaceMapScreen, "Space Map");
                    break;
                case ScreenId.Checkout:
                    (target, title) = (checkoutScreen, "Checkout");
                    break;
                case ScreenId.Management:
                case ScreenId.History:
                    adminScreen.ShowSection(AdminSection.History);
                    (target, title) = (adminScreen, "Administration");
                    break;
                case ScreenId.LeaseList:
                    adminScreen.ShowSection(AdminSection.Leases);
                    (target, title) = (adminScreen, "Administration");
                    break;
                case ScreenId.LeaseCancellation:
                    adminScreen.ShowSection(AdminSection.CancelLease);
                    (target, title) = (adminScreen, "Administration");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, "Unknown screen.");
            }

            // First hide the other screens, then display the target screen; the target screen loads its own data when it becomes visible
            foreach (Control control in contentPanel.Controls)
            {
                if (control != target) control.Visible = false;
            }
            target.Visible = true;
            lblScreenTitle.Text = title;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            controllers.Home();
        }

        // Return to Home Page: Discard any unfinished entry/settlement drafts and log out of the administrator account; this does not affect vehicles that have already entered.
        private void btnHome_Click(object sender, EventArgs e)
        {
            controllers.Home();
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            controllers.Dashboard.OpenArrival();
        }
    }
}
