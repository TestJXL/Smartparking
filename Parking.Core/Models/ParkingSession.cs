
namespace Parking.Core.Models
{
    public sealed class ParkingSession
    {
        // Primary key for parking records
        public int Sid { get; set; }

        // License plate number
        public string Carplate { get; set; } = "";

        // Which parking space to park in
        public int Pid { get; set; }

        // Entry time (UTC Unix seconds)
        public long StartTime { get; set; }

        // Does it include charging services? (No additional charge)
        public bool UseEV { get; set; }

        // Is this a large parking space? (20% surcharge)
        public bool IsBig { get; set; }

        // Is this the start of the long-term car
        public bool IsLeased { get; set; }

        // Is the process in progress? (true = the car is still parked)
        public bool IsActive { get; set; } = true;

        // If associated with a lease, this is the lease ID; otherwise, it is -1
        public int LeaseID { get; set; } = -1;

        // Exit time (UTC Unix seconds). null if still in progress.
        public long? EndTime { get; set; }

        // Snapshot of the hourly rate at the moment of entry
        public long HourlyRateCents { get; set; }

        // Actual amount paid
        public long AmountPaidCents { get; set; }

        // Was it manually removed by an administrator?
        public bool AdminRemoved { get; set; }

        // If removed by an administrator, the reason for removal is listed here
        public string CloseReason { get; set; } = "";
    }
}
