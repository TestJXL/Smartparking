namespace Parking.WinForms.Helpers
{
    // Common prompt text styles across all screens: red for errors, green for success
    public static class MessageExtensions
    {
        private static readonly Color ErrorColor = Color.Firebrick;
        private static readonly Color InfoColor = Color.SeaGreen;

        public static void ShowError(this Label label, string message)
        {
            label.ForeColor = ErrorColor;
            label.Text = message;
        }

        public static void ShowInfo(this Label label, string message)
        {
            label.ForeColor = InfoColor;
            label.Text = message;
        }

        // Allow only vertical scrolling: When a vertical scrollbar appears, the percentage-based column width will slightly exceed the visible width,
        public static void AllowVerticalScrollOnly(this ScrollableControl panel)
        {
            panel.AutoScroll = false;
            panel.HorizontalScroll.Maximum = 0;
            panel.HorizontalScroll.Visible = false;
            panel.AutoScroll = true;
        }
    }
}
