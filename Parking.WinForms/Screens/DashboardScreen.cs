using Parking.Controllers;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Screens
{    public partial class DashboardScreen : UserControl
    {
        private ParkingControllers? controllers;

        public DashboardScreen()
        {
            InitializeComponent();
            layoutRoot.AllowVerticalScrollOnly();
        }

        public void Bind(ParkingControllers parkingControllers)
        {
            controllers = parkingControllers;
        }

        private void LoadCounts()
        {
            if (controllers == null) return;

            var result = controllers.Dashboard.Load();
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }

            Dictionary<string, int> counts = result.Value!;
            int available = counts.GetValueOrDefault("AvailableCount");
            int enabled = counts.GetValueOrDefault("EnabledCount");
            int total = counts.GetValueOrDefault("TotalCount");

            lblAvailableCount.Text = available.ToString();
            lblEnabledCount.Text = enabled.ToString();
            lblTotalCount.Text = total.ToString();
            occupancyBar.SetCounts(available, enabled - available, total - enabled);
            lblMessage.Text = "";
        }

        // vechicle in & out use the same screen
        private void btnArrival_Click(object sender, EventArgs e)
        {
            controllers?.Dashboard.OpenArrival();
        }

        private void btnDeparture_Click(object sender, EventArgs e)
        {
            controllers?.Dashboard.OpenArrival();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCounts();
        }

        private void DashboardScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) LoadCounts(); //reload vechicle counts when back to dashboard
        }
    }
}
