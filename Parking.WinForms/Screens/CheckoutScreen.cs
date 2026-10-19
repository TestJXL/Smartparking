using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Screens
{
    //display the quote calculated by CheckoutController
    public partial class CheckoutScreen : UserControl
    {
        private ParkingControllers? controllers;

        public CheckoutScreen()
        {
            InitializeComponent();
            layoutRoot.AllowVerticalScrollOnly();
        }

        public void Bind(ParkingControllers parkingControllers)
        {
            controllers = parkingControllers;
        }

        private void LoadQuote()
        {
            if (controllers == null) return;

            var result = controllers.Checkout.Load();
            if (!result.Success)
            {
                lblTitle.Text = "Checkout";
                lblHint.Text = "Start from Vehicle Gate: enter the plate of a parked vehicle to check it out.";
                HideQuote();
                lblMessage.ShowError(result.Message);
                return;
            }
            ShowQuote(result.Value!);
        }

        private void ShowQuote(CheckoutQuote quote)
        {
            bool leasePayment = quote.Kind == CheckoutKind.FirstLeasePayment;
            switch (quote.Kind)
            {
                case CheckoutKind.FirstLeasePayment: //if first lease, show the month fee
                    lblTitle.Text = "Lease Payment";
                    lblHint.Text = $"First month's lease fee for space {quote.SpaceCode}. Paying starts the lease and parks the vehicle.";
                    btnPay.Text = "Pay Lease Fee && Enter";
                    break;
                case CheckoutKind.TenantExit: //if a normal tenant, no fees
                    lblTitle.Text = "Tenant Exit";
                    lblHint.Text = "This visit is covered by the vehicle's lease, so there is nothing to pay.";
                    btnPay.Text = "Confirm Exit";
                    break;
                default:
                    lblTitle.Text = "Checkout && Payment"; //if temp vechicle, fee = duration * hourly rate
                    lblHint.Text = "Check the fee below, then confirm the payment to release the space.";
                    btnPay.Text = "Pay && Exit";
                    break;
            }

            lblPlate.Text = quote.Plate;
            lblSpace.Text = quote.SpaceCode;
            lblEntry.Text = quote.EntryUtc.ToLocalDisplay();
            lblCheckout.Text = quote.CheckoutUtc.ToLocalDisplay();
            lblDuration.Text = quote.Duration.ToDurationText();
            lblHours.Text = quote.ChargeableHours.ToString();
            lblRate.Text = quote.HourlyRateCents.ToMoney() + " per hour";
            lblAmount.Text = quote.AmountCents.ToMoney();
            lblAmountCaption.Text = leasePayment ? "Monthly fee" : "Amount due";

            foreach (Control row in new Control[]
                     { //for the first month lease, only display plate, parking slot and amount.
                         lblEntryCaption, lblEntry, lblCheckoutCaption, lblCheckout, lblDurationCaption, lblDuration,
                         lblHoursCaption, lblHours, lblRateCaption, lblRate,
                     })
            {
                row.Visible = !leasePayment;
            }

            lblGraceNote.Text =
                $"Stays of up to {quote.GraceMinutes} minutes are free. Longer stays are charged for every started " +
                "hour of the whole visit – the free period is not deducted. Big spaces cost 20% more per hour.";
            lblGraceNote.Visible = quote.Kind == CheckoutKind.TemporaryExit;

            grpReceipt.Visible = false;
            btnDone.Visible = false;
            grpQuote.Visible = true;
            buttonRow.Visible = true;
            btnPay.Enabled = true;
        }

        private void HideQuote()
        {
            grpQuote.Visible = false;
            lblGraceNote.Visible = false;
            btnPay.Enabled = false;
            btnPay.Text = "Pay && Exit";
        }

        private void ShowReceipt(CheckoutReceipt receipt)
        //display receipt. for the first month's rent, show "Lease Started," and for other cases, show the exit time and duration.
        {
            bool leasePayment = receipt.Kind == CheckoutKind.FirstLeasePayment;
            lblReceipt.Text =
                $"Ticket no.  {receipt.SessionId}\n" +
                $"Plate       {receipt.Plate}\n" +
                $"Space       {receipt.SpaceCode}\n" +
                $"Entered     {receipt.EntryUtc.ToLocalDisplay()}\n" +
                (leasePayment
                    ? $"Lease       L-{receipt.LeaseId:0000} started\n"
                    : $"Exited      {receipt.ExitUtc.ToLocalDisplay()}\n" +
                      $"Duration    {receipt.Duration.ToDurationText()}\n") +
                $"Paid        {receipt.AmountPaidCents.ToMoney()}";

            HideQuote();
            buttonRow.Visible = false;
            grpReceipt.Visible = true;
            btnDone.Visible = true;
            lblMessage.ShowInfo(leasePayment
                ? $"Payment received. The lease has started – please park in space {receipt.SpaceCode}."
                : "Payment complete. The space has been released.");
            btnDone.Focus();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (controllers == null) return;

            //disable button when paying to prevent duplicate payment
            btnPay.Enabled = false;
            var result = controllers.Checkout.Confirm();
            if (result.Success)
            {
                ShowReceipt(result.Value!);
                return;
            }

            //if the amount crosses a threshold, must generate a new quote and reconfirmation
            if (result.Error is ErrorCode.QuoteChanged or ErrorCode.QuoteExpired)
            {
                LoadQuote();
            }
            else
            {
                btnPay.Enabled = grpQuote.Visible;
            }
            lblMessage.ShowError(result.Message);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            controllers?.Checkout.Cancel();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            controllers?.Checkout.Done();
        }

        private void CheckoutScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible) return;
            lblMessage.Text = "";
            grpReceipt.Visible = false;
            btnDone.Visible = false;
            buttonRow.Visible = true;
            LoadQuote();
        }
    }
}
