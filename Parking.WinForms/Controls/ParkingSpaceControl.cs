using System.ComponentModel;
using System.Drawing.Drawing2D;
using Parking.Core.Enums;
using Parking.WinForms.Helpers;

namespace Parking.WinForms.Controls
{
    // A parking space. Colors are always displayed alongside status text; “Recommended” is not a status and is indicated separately with a star and a blue outline.
    public class ParkingSpaceControl : Control
    {
        private static readonly Color RecommendColor = Color.FromArgb(30, 110, 220);
        private static readonly Color SelectedColor = Color.FromArgb(20, 40, 90);
        private static readonly Color InactiveBack = Color.FromArgb(225, 225, 225);
        private static readonly Color InactiveBorder = Color.Gray;

        private string code = "";
        private VehicleSize spaceSize;
        private bool hasCharger;
        private SpaceStatus status;
        private bool isActive = true;
        private bool isSelectable;
        private bool isRecommended;
        private bool isSelected;

        public ParkingSpaceControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 9F);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SpaceId { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Code
        {
            get => code;
            set { code = value; UpdateAppearance(); }
        }

        
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VehicleSize SpaceSize
        {
            get => spaceSize;
            set { spaceSize = value; UpdateAppearance(); }
        }

        // Does the parking space have a charging station?
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasCharger
        {
            get => hasCharger;
            set { hasCharger = value; UpdateAppearance(); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpaceStatus Status
        {
            get => status;
            set { status = value; UpdateAppearance(); }
        }

        // Disabled parking spaces are displayed in gray
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveSpace
        {
            get => isActive;
            set { isActive = value; UpdateAppearance(); }
        }

        // Can this vehicle select this parking space? (Determined by the service; for example, large vehicles cannot select spaces for small vehicles, and tenants can only select their own parking spaces.)
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelectable
        {
            get => isSelectable;
            set { isSelectable = value; Cursor = value ? Cursors.Hand : Cursors.Default; UpdateAppearance(); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRecommended
        {
            get => isRecommended;
            set { isRecommended = value; UpdateAppearance(); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => isSelected;
            set { isSelected = value; UpdateAppearance(); }
        }

        // Status text displayed in the grid
        public string StatusText
        {
            get
            {
                if (!isActive) return "Inactive";
                if (status == SpaceStatus.Available && !isSelectable) return "Not suitable";
                return status.ToDisplayText();
            }
        }

        // State color schemes; the legend also uses these two methods to ensure that colors are defined only once here
        public static Color StatusBackColor(SpaceStatus status)
        {
            return status switch
            {
                SpaceStatus.Occupied => Color.FromArgb(249, 218, 218),
                SpaceStatus.Leased => Color.FromArgb(255, 241, 204),
                _ => Color.FromArgb(222, 243, 228),
            };
        }

        public static Color StatusBorderColor(SpaceStatus status)
        {
            return status switch
            {
                SpaceStatus.Occupied => Color.FromArgb(192, 57, 43),
                SpaceStatus.Leased => Color.FromArgb(201, 152, 20),
                _ => Color.FromArgb(46, 139, 87),
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int inset = LogicalToDeviceUnits(3);
            var box = new Rectangle(inset, inset, Width - inset * 2 - 1, Height - inset * 2 - 1);
            if (box.Width <= 0 || box.Height <= 0) return;

            Color back = isActive ? StatusBackColor(status) : InactiveBack;
            Color statusColor = isActive ? StatusBorderColor(status) : InactiveBorder;
            // Available but not suitable for this car: The color has faded, indicating it cannot be selected
            bool dimmed = isActive && status == SpaceStatus.Available && !isSelectable;
            if (dimmed) back = ControlPaint.Light(back, 0.6f);

            using (GraphicsPath path = RoundedRectangle(box, LogicalToDeviceUnits(8)))
            {
                using (var fill = new SolidBrush(back))
                {
                    g.FillPath(fill, path);
                }

                // Selected > Recommended > Regular
                Color borderColor = isSelected ? SelectedColor
                    : isRecommended ? RecommendColor
                    : dimmed ? InactiveBorder : statusColor;
                float borderWidth = LogicalToDeviceUnits(isSelected ? 4 : isRecommended ? 3 : 1);
                using (var pen = new Pen(borderColor, borderWidth))
                {
                    if (dimmed) pen.DashStyle = DashStyle.Dash;
                    g.DrawPath(pen, path);
                }
            }

            int pad = LogicalToDeviceUnits(7);
            var textArea = Rectangle.Inflate(box, -pad, -pad / 2);
            string sizeText = spaceSize.ToDisplayText() + (hasCharger ? " · EV" : "");
            using (var bold = new Font(Font, FontStyle.Bold))
            {
                TextRenderer.DrawText(g, code, bold, textArea, Color.Black,
                    TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.EndEllipsis);
                if (isRecommended)
                {
                    TextRenderer.DrawText(g, "★", bold, textArea, RecommendColor,
                        TextFormatFlags.Right | TextFormatFlags.Top);
                }
            }
            TextRenderer.DrawText(g, sizeText, Font, textArea, Color.DimGray,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(g, StatusText, Font, textArea, dimmed ? Color.Gray : statusColor,
                TextFormatFlags.Left | TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis);
        }

        private void UpdateAppearance()
        {
            AccessibleName = $"Space {code}, {spaceSize.ToDisplayText()}{(hasCharger ? ", EV charger" : "")}, {StatusText}"
                + (isRecommended ? ", recommended" : "")
                + (isSelected ? ", selected" : "");
            Invalidate();
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
