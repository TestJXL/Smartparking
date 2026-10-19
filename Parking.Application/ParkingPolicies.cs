using Parking.Core.DTOs;
using Parking.Core.Enums;
using Parking.Core.Interfaces;
using Parking.Core.Models;

namespace Parking.Application;

public sealed class SystemClock : IClock { public DateTime UtcNow => DateTime.UtcNow; }

// Free time is a threshold and is not deducted from the total duration. 11 minutes = 1 hour; 61 minutes = 2 hours.
public sealed class HourlyFeePolicy : IFeePolicy
{
    public long CalculateCents(TimeSpan duration, long hourlyRateCents, int graceMinutes)
    {
        if (duration < TimeSpan.Zero || hourlyRateCents < 0 || graceMinutes < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));
        if (duration.TotalMinutes <= graceMinutes) return 0;
        return checked((long)Math.Ceiling(duration.TotalHours) * hourlyRateCents);
    }
}
public sealed class LeaseFeePolicy : IFeePolicy
{
    public long CalculateCents(TimeSpan duration, long hourlyRateCents, int graceMinutes)
    {
        if (duration < TimeSpan.Zero || hourlyRateCents < 0 || graceMinutes < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));
        return 0;
    }
}

public sealed class SpaceRecommendationService : ISpaceRecommendationService
{
    public SpaceRecommendation? Recommend(ParkingRequest request, IReadOnlyList<ParkingSpace> candidates)
    {
        // Matching priority: +1 for EV mismatch, +1 for a small car occupying a large space; large cars cannot park in small spaces.
        var best = candidates
            .Where(s => request.Size != VehicleSize.Big || s.IsBig)
            .OrderBy(s => (s.IsEV != request.IsEV ? 1 : 0) + (s.IsBig != (request.Size == VehicleSize.Big) ? 1 : 0))
            .ThenBy(s => s.Dest).ThenBy(s => s.Pid).FirstOrDefault();
        return best == null ? null : new SpaceRecommendation
        {
            SpaceId = best.Pid, Code = best.Code, DistanceMetres = best.Dest,
            Reason = "First sort by vehicle/parking space match, then by “Dest” and parking space ID."
        };
    }
}
