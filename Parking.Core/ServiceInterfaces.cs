using Parking.Core.DTOs;
using Parking.Core.Models;
namespace Parking.Core.Interfaces;

// Polymorphism: The hourly rental policy and the long-term rental policy calculate charges separately.
public interface IFeePolicy
{
    long CalculateCents(TimeSpan duration, long hourlyRateCents, int graceMinutes);
}
public interface ISpaceRecommendationService
{
    // Returns only the best match; candidates are filtered internally by the business service. Returns null if no candidates are found.
    SpaceRecommendation? Recommend(ParkingRequest request, IReadOnlyList<ParkingSpace> candidates);
}
public interface IClock { DateTime UtcNow { get; } }

// Business entry point for View/Controller; does not expose DbContext or writable entities.
public interface IParkingApplicationService
{
    OperationResult<Dictionary<string, int>> GetDashboardCounts();
    OperationResult<ArrivalDecision> ResolveArrival(string input);
    OperationResult<ParkingRequest> ValidateRegistration(ParkingRequest request);
    OperationResult<SpaceSelectionState> GetSelection(ParkingRequest request, int? selectedId = null);
    OperationResult<ParkingTicket> ConfirmParking(ParkingRequest request, int spaceId);
    OperationResult<CheckoutQuote> PreviewFirstLease(ParkingRequest request, int spaceId);
    OperationResult<CheckoutQuote> PreviewCheckout(int sessionId);
    OperationResult<CheckoutReceipt> ConfirmCheckout(Guid quoteId);
    void DiscardQuote(Guid quoteId);
    bool IsAdministrator { get; }
    void LogoutAdministrator();
    OperationResult<IReadOnlyList<HistoryRecord>> GetHistory();
    OperationResult<IReadOnlyList<LeaseInfo>> GetLeases(bool activeOnly = false);
    OperationResult CancelLease(int leaseId);
}
