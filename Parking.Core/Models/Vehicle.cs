
namespace Parking.Core.Models
{
    public sealed class Vehicle
    {
        // Is it an electric vehicle? (Used to display the charging icon on the interface, etc.)
        public bool IsEV { get; set; }

        // Is it a large vehicle (SUV, pickup truck, etc.)?
        public bool IsBig { get; set; }

        // License plate number
        public string Carplate { get; set; } = "";

        // If this vehicle was admitted under a lease agreement, enter the lease ID here; otherwise, enter -1
        public int LeaseID { get; set; } = -1;

        // Points to the current parking record (ParkingSession.Sid)
        public int SessionID { get; set; } = -1;
    }
}
