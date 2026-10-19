using Parking.Controllers;
using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.WinForms.Controls;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Screens
{
    //parking space map, use MapRow & MapColumn to place parking slot
    //recommendation and selected slots displayed on the right
    public partial class SpaceMapScreen : UserControl
    {
        private const int TileWidth = 104;
        private const int TileHeight = 76;
        private const int TileGap = 10;

        private readonly Dictionary<int, ParkingSpaceControl> tiles = new();
        private ParkingControllers? controllers;
        private SpaceSelectionState? state;

        public SpaceMapScreen()
        {
            InitializeComponent();

            //legend colors and the parking space use the same color set
            SetLegend(lblLegendAvailable, SpaceStatus.Available);
            SetLegend(lblLegendLeased, SpaceStatus.Leased);
            SetLegend(lblLegendOccupied, SpaceStatus.Occupied);
            lblLegendNotSuitable.BackColor = ControlPaint.Light(ParkingSpaceControl.StatusBackColor(SpaceStatus.Available), 0.6f);
        }

        public void Bind(ParkingControllers parkingControllers)
        {
            controllers = parkingControllers;
        }

        private void LoadSelection()
        {
            if (controllers == null) return;

            var result = controllers.SpaceSelection.Load(); //load space map from backend
            if (!result.Success)
            {
                ShowState(null);
                lblMessage.ShowError(result.Message);
                return;
            }
            ShowState(result.Value);
        }

        private void SelectSpace(int spaceId)
        {
            if (controllers == null) return;

            var result = controllers.SpaceSelection.Select(spaceId);
            if (!result.Success)
            {
                lblMessage.ShowError(result.Message);
                return;
            }
            lblMessage.Text = "";
            ShowState(result.Value);
            if (tiles.TryGetValue(spaceId, out var tile)) mapPanel.ScrollControlIntoView(tile);
        }

        private void ShowState(SpaceSelectionState? newState)
        {
            state = newState;
            IReadOnlyList<SpaceInfo> spaces = newState?.Spaces ?? Array.Empty<SpaceInfo>();

            //only update grid status if the slots set haven't been changed
            if (!tiles.Keys.ToHashSet().SetEquals(spaces.Select(s => s.Id)))
            {
                RebuildTiles(spaces);
            }
            foreach (SpaceInfo space in spaces)
            {
                ParkingSpaceControl tile = tiles[space.Id];
                tile.Code = space.Code;
                tile.SpaceSize = space.Size;
                tile.HasCharger = space.IsEV;
                tile.Status = space.Status;
                tile.IsActiveSpace = space.IsActive;
                tile.IsSelectable = space.CanSelect;
                tile.IsRecommended = newState?.Recommendation?.SpaceId == space.Id;
                tile.IsSelected = newState?.SelectedSpaceId == space.Id;
            }

            SpaceRecommendation? recommendation = newState?.Recommendation;
            if (recommendation == null)
            {
                lblRecommendation.Text = newState == null ? "" : "No suitable space is free for this vehicle right now.";
                btnUseRecommended.Enabled = false; //if there is no recommended slot, recommend button should not be clicked
            }
            else
            {
                lblRecommendation.Text = $"Space {recommendation.Code}\n{recommendation.Reason}";
                btnUseRecommended.Enabled = recommendation.SpaceId != newState!.SelectedSpaceId;
            }

            lblSelected.Text = newState?.SelectedSpaceId is int id && tiles.TryGetValue(id, out var selected)
                ? $"{selected.Code} · {selected.SpaceSize.ToDisplayText()} space{(selected.HasCharger ? " with EV charger" : "")} · {selected.StatusText}"
                : "No space selected. Click a highlighted space on the map.";
            btnConfirm.Enabled = newState?.SelectedSpaceId != null;
        }

        private void RebuildTiles(IReadOnlyList<SpaceInfo> spaces)
        {
            mapPanel.SuspendLayout(); //pause calculations until controlls done processed, call ResumeLayout to recalculate
            foreach (ParkingSpaceControl old in tiles.Values)
            {
                old.Click -= Tile_Click; //remove click event
                mapPanel.Controls.Remove(old);
                old.Dispose(); //recycle resource
            }
            tiles.Clear();

            //calculate dimensions and scroll offsets
            int width = LogicalToDeviceUnits(TileWidth);
            int height = LogicalToDeviceUnits(TileHeight);
            int gap = LogicalToDeviceUnits(TileGap);
            Point scroll = mapPanel.AutoScrollPosition;

            foreach (SpaceInfo space in spaces)
            {
                var tile = new ParkingSpaceControl
                {
                    SpaceId = space.Id,
                    Size = new Size(width, height),
                    //position of new control relative to the current scroll must add offset
                    Location = new Point(gap + space.MapColumn * (width + gap) + scroll.X,
                                         gap + space.MapRow * (height + gap) + scroll.Y),
                };
                tile.Click += Tile_Click;
                tiles.Add(space.Id, tile);
                mapPanel.Controls.Add(tile);
            }
            mapPanel.ResumeLayout();
        }

        private void Tile_Click(object? sender, EventArgs e)
        {
            if (sender is not ParkingSpaceControl tile) return;

            if (!tile.IsSelectable)
            {
                lblMessage.ShowError($"Space {tile.Code} is {tile.StatusText.ToLowerInvariant()} and cannot be chosen for this vehicle.");
                return;
            }
            SelectSpace(tile.SpaceId);
        }

        private void btnUseRecommended_Click(object sender, EventArgs e)
        {
            if (state?.Recommendation != null) SelectSpace(state.Recommendation.SpaceId);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (controllers == null) return;

            btnConfirm.Enabled = false; //disable button during processing to prevent double click
            var result = controllers.SpaceSelection.Next();
            if (!result.Success)
            {
                LoadSelection();
                lblMessage.ShowError(result.Message);
                return;
            }

            //for new lease, jump to checkout screen for payment; for temp vechicle, issue ticket
            ParkingTicket? ticket = result.Value!.Ticket;
            if (ticket != null)
            {
                MessageBox.Show(FindForm(),
                    "Parking confirmed.\n\n" +
                    $"Ticket no.:  {ticket.SessionId}\n" +
                    $"Plate:  {ticket.Plate}\n" +
                    $"Space:  {ticket.SpaceCode}\n" +
                    $"Entered:  {ticket.EntryUtc.ToLocalDisplay()}\n" +
                    $"Billing:  {ticket.FeePolicy.ToDisplayText()}",
                    "Parking ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LoadSelection();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            controllers?.Home();
        }

        private void SpaceMapScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible) return;
            lblMessage.Text = "";
            LoadSelection();
        }

        private static void SetLegend(Label label, SpaceStatus status)
        {
            label.BackColor = ParkingSpaceControl.StatusBackColor(status);
            label.ForeColor = ParkingSpaceControl.StatusBorderColor(status);
        }
    }
}
