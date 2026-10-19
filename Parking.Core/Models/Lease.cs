namespace Parking.Core.Models
{
    public sealed class Lease
    {
        // Lease primary key (system-generated ID)
        public int Lid { get; set; }

        // Assigned parking space ID
        public int Pid { get; set; }

        // Tenant Name
        public string Name { get; set; } = "";

        // Tenant contact information (phone number or email address)
        public string Contact { get; set; } = "";

        // Tenant Address
        public string Address { get; set; } = "";

        // Tenant's license plate number
        public string Carplate { get; set; } = "";

        // 4-digit PIN
        public int Pin { get; set; }

        // Does the assigned parking space have a charging station?
        public bool SpaceIsEV { get; set; }

        // Is the tenant's vehicle an electric vehicle
        public bool CarIsEV { get; set; }

        // Is the tenant's vehicle a large vehicle?
        public bool IsBig { get; set; }

        // Is the lease still valid? (true = not canceled)
        public bool IsActive { get; set; } = true;

        // Does the rental agreement include charging fees (so no additional charging fees are charged at checkout)?
        public bool UseEV { get; set; }

        // Lease start time (UTC Unix seconds)
        public long StartTime { get; set; }

        // Lease expiration date
        public long EndTime { get; set; }

        // If canceled, this is the cancellation time; otherwise, it is null
        public long? CancelledTime { get; set; }

        // Prepaid lease expenses
        public long FeePaidCents { get; set; }
    }
}
