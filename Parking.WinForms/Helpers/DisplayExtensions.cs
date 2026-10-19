using System.Globalization;
using Parking.Core.Enums;

namespace Parking.WinForms.Helpers
{
    // Extension methods for display purposes. In the interface, amounts are in Australian cents and times are in UTC; only the form is responsible for converting them into human-readable text.
    public static class DisplayExtensions
    {
        private static readonly CultureInfo Australia = CultureInfo.GetCultureInfo("en-AU");

        // 500 → "$5.00"
        public static string ToMoney(this long cents)
        {
            return (cents / 100m).ToString("C", Australia);
        }

        // UTC → Local Time Text
        public static string ToLocalDisplay(this DateTime utc)
        {
            return DateTime.SpecifyKind(utc, DateTimeKind.Utc).ToLocalTime().ToString("dd/MM/yyyy HH:mm", Australia);
        }

        // Nullable version: Displays “—” in cases such as when the performer has not yet left the stage
        public static string ToLocalDisplay(this DateTime? utc)
        {
            return utc.HasValue ? utc.Value.ToLocalDisplay() : "—";
        }

        // 2 hours 20 minutes → “2 h 20 min”
        public static string ToDurationText(this TimeSpan duration)
        {
            if (duration < TimeSpan.Zero) duration = TimeSpan.Zero;
            int hours = (int)duration.TotalHours;
            return hours > 0 ? $"{hours} h {duration.Minutes} min" : $"{duration.Minutes} min";
        }

        public static string ToDisplayText(this SpaceStatus status)
        {
            return status switch
            {
                SpaceStatus.Available => "Available",
                SpaceStatus.Occupied => "Occupied",
                SpaceStatus.Leased => "Leased",
                _ => status.ToString(),
            };
        }

        public static string ToDisplayText(this VehicleSize size)
        {
            return size == VehicleSize.Big ? "Big" : "Small";
        }

        public static string ToDisplayText(this FeePolicyKind policy)
        {
            return policy == FeePolicyKind.Lease ? "Lease (covered)" : "Hourly";
        }
    }
}
