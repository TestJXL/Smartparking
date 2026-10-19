

namespace Parking.Core
{
    // Global constants. All constants related to default values and range limits are listed here.
    public static class Constants
    {

        // Rate-related
        // Default hourly rate (Australian cents per hour). Default: $5.00/hour.
        public const long DefaultHourlyRateCents = 500;

        //Default number of free minutes. Parking is free as long as it does not exceed this duration.
        public const int DefaultGraceMinutes = 10;

        // Minimum hourly rate (Australian cents). 0 indicates free parking.
        public const long MinHourlyRateCents = 0;

        // Maximum hourly rental rate (cents per hour). 100,000 = $1,000 per hour.
        public const long MaxHourlyRateCents = 100_000;

        // The minimum number of free minutes.
        public const int MinGraceMinutes = 0;

        // Maximum number of free minutes.
        public const int MaxGraceMinutes = 120;

        // License Plate Format
        // Minimum length (number of characters) allowed after license plate normalization.
        public const int MinPlateLength = 2;

        // Maximum length (number of characters) allowed after license plate standardization.
        public const int MaxPlateLength = 8;


        // Lease-related
        // Minimum value for the lease PIN (4-digit number).
        public const int MinLeasePin = 1000;

        // Maximum value for the lease PIN (4-digit number).
        public const int MaxLeasePin = 9999;

        // Prefix for the lease agreement number, e.g., "L-0007".
        public const string LeaseReferencePrefix = "L-";

        // The number of digits in the numeric portion of the lease number. For example, 4 digits would range from L-0001 to L-9999.
        public const int LeaseReferenceDigits = 4;

        // Parking Log-Related
        // The number of seconds in an hour. Used for time calculations to avoid hard-coding 3600.
        public const long SecondsPerHour = 3600;

        // Number of seconds in a day.
        public const long SecondsPerDay = 86_400;
    }
}
