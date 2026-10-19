using Parking.Core.Enums;

namespace Parking.WinForms.Controls
{
    // Horizontal stacked bar chart showing parking space status: available / in use or rented / out of service; colors match those of the parking space tiles.
    public class OccupancyBar : Control
    {
        private static readonly Color InactiveBack = Color.FromArgb(225, 225, 225);

        private int availableCount;
        private int unavailableCount;
        private int inactiveCount;

        public OccupancyBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        public void SetCounts(int available, int unavailable, int inactive)
        {
            availableCount = Math.Max(0, available);
            unavailableCount = Math.Max(0, unavailable);
            inactiveCount = Math.Max(0, inactive);
            AccessibleName = $"Available {availableCount}, in use or leased {unavailableCount}, inactive {inactiveCount}";
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            int total = availableCount + unavailableCount + inactiveCount;

            if (total == 0)
            {
                g.FillRectangle(SystemBrushes.ControlLight, area);
                TextRenderer.DrawText(g, "No spaces", Font, area, Color.DimGray,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            var segments = new[]
            {
                (Count: unavailableCount,
                 Back: ParkingSpaceControl.StatusBackColor(SpaceStatus.Occupied),
                 Border: ParkingSpaceControl.StatusBorderColor(SpaceStatus.Occupied)),
                (Count: availableCount,
                 Back: ParkingSpaceControl.StatusBackColor(SpaceStatus.Available),
                 Border: ParkingSpaceControl.StatusBorderColor(SpaceStatus.Available)),
                (Count: inactiveCount, Back: InactiveBack, Border: Color.Gray),
            }.Where(s => s.Count > 0).ToList();

            int x = 0;
            for (int i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                // The last paragraph accounts for rounding errors
                int width = i == segments.Count - 1
                    ? area.Width - x
                    : (int)Math.Round(area.Width * (double)segment.Count / total);
                var rect = new Rectangle(x, 0, width, area.Height);

                using (var brush = new SolidBrush(segment.Back))
                {
                    g.FillRectangle(brush, rect);
                }
                using (var pen = new Pen(segment.Border))
                {
                    g.DrawRectangle(pen, rect.X, rect.Y, Math.Max(0, rect.Width - 1), Math.Max(0, rect.Height));
                }

                string label = $"{Math.Round(segment.Count * 100.0 / total)}%";
                if (TextRenderer.MeasureText(label, Font).Width + 4 < rect.Width)
                {
                    TextRenderer.DrawText(g, label, Font, rect, segment.Border,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                x += width;
            }
        }
    }
}
