using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Screens
{
    public partial class PlateEntryScreen : UserControl
    {
        private ParkingControllers? controllers;

        public PlateEntryScreen()
        {
            InitializeComponent();
            layoutRoot.AllowVerticalScrollOnly();
        }

        public void Bind(ParkingControllers parkingControllers)
        {
            controllers = parkingControllers;
        }

        private void ShowPlateStep() //display carplate entry
        {
            txtPlateOrToken.ReadOnly = false;
            btnContinue.Enabled = true;
            grpNewCar.Visible = false;
            grpLease.Visible = false;
            detailsButtons.Visible = false;
            txtPlateOrToken.Focus();
        }

        private void ShowDetailsStep(string plate)  //show new vechicle's details
        {
            txtPlateOrToken.Text = plate;
            txtPlateOrToken.ReadOnly = true;
            btnContinue.Enabled = false;
            grpNewCar.Visible = true;
            grpLease.Visible = chkLease.Checked;
            detailsButtons.Visible = true;
            lblMessage.ShowInfo($"{plate} is a new vehicle. Choose the options below, then press Choose a Space.");
        }

        private void ClearInputs()
        {
            txtPlateOrToken.Clear();
            rbSmall.Checked = true;
            chkEV.Checked = false;
            chkLease.Checked = false;
            txtName.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            lblMessage.Text = "";
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (controllers == null) return;
            if (string.IsNullOrWhiteSpace(txtPlateOrToken.Text)) //check if user input is valid
            {
                lblMessage.ShowError("Please enter a number plate.");
                txtPlateOrToken.Focus();
                return;
            }

            var result = controllers.Arrival.SubmitPlate(txtPlateOrToken.Text); //give the obtained plate to backend for verification
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }
            if (result.Value!.Route == ArrivalRoute.MoreInformation)
            {
                ShowDetailsStep(result.Value.Plate); //if verification from backend is good, show the details
            }
        }

        private void btnFindSpace_Click(object sender, EventArgs e)
        {
            if (controllers == null) return;
            if (chkLease.Checked &&  //check if this plate is a valid tenant
                (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtContact.Text) ||
                 string.IsNullOrWhiteSpace(txtAddress.Text)))
            {
                lblMessage.ShowError("Please enter the tenant's name, contact and address.");
                return;
            }

            var request = new ParkingRequest
            {
                Size = rbBig.Checked ? VehicleSize.Big : VehicleSize.Small,
                IsEV = chkEV.Checked,
                RegisterLease = chkLease.Checked,
                Name = txtName.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Address = txtAddress.Text.Trim(),
            };
            var result = controllers.Arrival.SubmitDetails(request);
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
            }
        }

        private void btnChangePlate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            ShowPlateStep();
        }

        private void txtPlateOrToken_KeyDown(object sender, KeyEventArgs e) //press enter = click continue
        {
            if (e.KeyCode == Keys.Enter && !txtPlateOrToken.ReadOnly)
            {
                e.SuppressKeyPress = true;
                btnContinue.PerformClick();
            }
        }

        private void chkLease_CheckedChanged(object sender, EventArgs e)
        {
            grpLease.Visible = chkLease.Checked && grpNewCar.Visible;
        }

        private void PlateEntryScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible) return;
            ClearInputs();
            ShowPlateStep();
        }
    }
}
