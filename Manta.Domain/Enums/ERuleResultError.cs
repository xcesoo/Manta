namespace Manta.Domain.Enums;

public enum ERuleResultError
{
    Unknown,
    ParcelAlreadyDelivered,
    ParcelWrongStatus,
    LocationMismatch,
    ParcelAlreadyRightLocation,
    StorageTimeExpired,
    PaymentRequired,
    
}