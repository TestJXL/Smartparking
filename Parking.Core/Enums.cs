namespace Parking.Core.Enums;
public enum VehicleSize { Small, Big }
public enum SpaceStatus { Available, Occupied, Leased }
public enum FeePolicyKind { Hourly, Lease }
public enum ScreenId { Dashboard, Registration, SpaceMap, Checkout, Management, History, LeaseList, LeaseCancellation }
public enum ArrivalRoute { MoreInformation, TenantSpace, Checkout, Management }
public enum CheckoutKind { TemporaryExit, TenantExit, FirstLeasePayment }
public enum SpaceCellColor { White, Green, Red }
public enum ErrorCode
{
    InvalidInput, InvalidPlate, VehicleAlreadyParked, NoSuitableSpace, SpaceNotFound,
    SpaceUnavailable, SpaceIncompatible, LeaseNotFound, LeaseNotValid, LeaseConflict,
    SessionNotFound, SessionAlreadyClosed, Unauthorized, QuoteExpired, QuoteChanged,
    StorageConflict, Unexpected
}
