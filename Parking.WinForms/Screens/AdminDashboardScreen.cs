using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Screens
{
    public enum AdminSection { History, Leases, CancelLease }

    //administrator Dashboard has 3 tabs: records, lease list, and cancel lease.
    public partial class AdminDashboardScreen : UserControl
    {
        private ParkingControllers? controllers;
        private IReadOnlyList<HistoryRecord> history = Array.Empty<HistoryRecord>();
        private IReadOnlyList<LeaseInfo> leases = Array.Empty<LeaseInfo>();

        public AdminDashboardScreen()
        {
            InitializeComponent();
            dgvHistory.SetupReadOnly();
            dgvLeases.SetupReadOnly();
        }

        public void Bind(ParkingControllers parkingControllers)
        {
            controllers = parkingControllers;
        }

        public void ShowSection(AdminSection section)
        {  //switch to the specified tab and refresh the data
            int index = (int)section;
            if (tabs.SelectedIndex != index)
            {
                tabs.SelectedIndex = index; //change the SelectedIndex triggers `tabs_SelectedIndexChanged` to loads the data.
            }
            else
            {
                LoadCurrentSection();
            }
        }

        private void LoadCurrentSection()
        {     //load releveant data to the current tab and if the page is hidden, no data will be loaded
            if (controllers == null || !Visible) return;

            lblMessage.Text = "";
            switch ((AdminSection)tabs.SelectedIndex)
            {
                case AdminSection.History:
                    LoadHistory();
                    break;
                case AdminSection.Leases:
                    LoadLeases();
                    break;
                case AdminSection.CancelLease:
                    LoadActiveLeases();
                    break;
            }
        }

        private void LoadHistory()
        {
            var result = controllers!.History.Load();
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }
            history = result.Value!;
            ShowHistory();
        }

        private void ShowHistory()
        { //filtering by plate that are already retrieved and it does not trigger a new query.
            string filter = txtPlateFilter.Text.Trim();
            List<HistoryRecord> shown = history
                .Where(r => filter.Length == 0 || r.Plate.Contains(filter, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dgvHistory.DataSource = shown   //convert to anonymous object and the table column names and display text are both determined here
                .Select(r => new
                {
                    Ticket = r.SessionId,
                    Plate = r.Plate,
                    Space = r.SpaceCode,
                    Entered = r.EntryUtc.ToLocalDisplay(),
                    Exited = r.ExitUtc.ToLocalDisplay(),
                    Status = r.IsActive ? "Parked" : r.AdminRemoved ? "Removed by admin" : "Completed",
                    Type = r.IsLease ? "Lease" : "Hourly",
                    Paid = r.AmountPaidCents.ToMoney(),
                })
                .ToList();

            int parked = shown.Count(r => r.IsActive);
            lblHistorySummary.Text = $"{shown.Count} of {history.Count} visits shown · {parked} currently parked";
        }

        private void txtPlateFilter_TextChanged(object sender, EventArgs e)
        { 
            ShowHistory();  //refilter when every letter entered
        }

        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadCurrentSection();
        }

        private void LoadLeases()
        {
            var result = controllers!.LeaseList.Load();
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }
            leases = result.Value!;
            ShowLeases();
        }
        private void ShowLeases()
        {  //if "Show active only" is checked, cancelled leases are hidden
            List<LeaseInfo> shown = leases.Where(l => !chkActiveOnly.Checked || l.IsActive).ToList();

            dgvLeases.DataSource = shown
                .Select(l => new
                {
                    Lease = l.Reference,
                    Plate = l.Plate,
                    Tenant = l.Name,
                    Contact = l.Contact,
                    Vehicle = l.VehicleSize.ToDisplayText() + (l.CarIsEV ? " · EV" : ""),
                    Space = l.SpaceCode,
                    Started = l.StartUtc.ToLocalDisplay(),
                    Status = l.IsActive ? "Active" : "Cancelled " + l.CancelledUtc.ToLocalDisplay(),
                    Paid = l.FeePaidCents.ToMoney(),
                })
                .ToList();

            lblLeaseSummary.Text = $"{shown.Count} of {leases.Count} leases shown · {leases.Count(l => l.IsActive)} active";
        }

        private void chkActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            ShowLeases();
        }

        private void btnRefreshLeases_Click(object sender, EventArgs e)
        {
            LoadCurrentSection();
        }

        private void LoadActiveLeases()
        {
            var result = controllers!.LeaseCancellation.Load();
            lstActiveLeases.Items.Clear(); //firstly clear list and details，so that if load failed, old data wont be kept
            ShowLeaseDetails(null);
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }
            foreach (LeaseInfo lease in result.Value!)
            {
                lstActiveLeases.Items.Add(new LeaseOption(lease));
            }
            lblActiveLeases.Text = $"Active leases ({result.Value!.Count}):";
        }
        private void ShowLeaseDetails(LeaseInfo? lease)
        {
            btnCancelLease.Enabled = lease != null;
            lblLeaseDetails.Text = lease == null
                ? "Select a lease on the left."
                : $"Lease     {lease.Reference}\n" +
                  $"Plate     {lease.Plate}\n" +
                  $"Tenant    {lease.Name}\n" +
                  $"Contact   {lease.Contact}\n" +
                  $"Address   {lease.Address}\n" +
                  $"Space     {lease.SpaceCode}\n" +
                  $"Started   {lease.StartUtc.ToLocalDisplay()}\n" +
                  $"Fee paid  {lease.FeePaidCents.ToMoney()}";
        }
        private void lstActiveLeases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controllers == null || lstActiveLeases.SelectedItem is not LeaseOption option)
            {
                ShowLeaseDetails(null);
                return;
            }

            var result = controllers.LeaseCancellation.Select(option.Lease.LeaseId);
            if (!result.Success)
            {
                ShowLeaseDetails(null);
                lblMessage.ShowError(result.Message);
                return;
            }
            ShowLeaseDetails(result.Value);
        }

        private void btnCancelLease_Click(object sender, EventArgs e)
        {
            if (controllers == null || lstActiveLeases.SelectedItem is not LeaseOption option) return;

            //cannt undo cancellation. default button is `no` to prevent accidental enter pressed
            LeaseInfo lease = option.Lease;
            DialogResult answer = MessageBox.Show(FindForm(),
                $"Cancel lease {lease.Reference} for {lease.Plate} (space {lease.SpaceCode})?\n\nThis cannot be undone.",
                "Cancel lease", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (answer != DialogResult.Yes) return;


            var result = controllers.LeaseCancellation.Cancel(lease.LeaseId);
            LoadActiveLeases();  //whether successed or failed, reload data anyway
            if (result.Success)
            {
                lblMessage.ShowInfo($"Lease {lease.Reference} has been cancelled and space {lease.SpaceCode} released.");
            }
            else
            {
                lblMessage.ShowError(result.Message);
            }
        }

        private void btnRefreshActive_Click(object sender, EventArgs e)
        {
            LoadCurrentSection();
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCurrentSection();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            controllers?.Management.Logout(); //exit admin and back to homepage
        }

        private void AdminDashboardScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) LoadCurrentSection(); //everytime enter this page, reload data
        }
        private sealed record LeaseOption(LeaseInfo Lease)
        {
            public override string ToString() => $"{Lease.Reference}  ·  {Lease.Plate}  ·  space {Lease.SpaceCode}  ·  {Lease.Name}";
        }
    }
}
