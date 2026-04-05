namespace Manta.Contracts;

public record CreateParcelMessage
    (
        Guid ParcelId,
        Guid MessageId,
        Guid DeliveryPointId,
        Guid CreatedById,
        string CreatedByEmail,
        string CreatedByName,
        string CreatedByRole,
        decimal AmountDue,
        double Weight,
        string RecipientName,
        string RecipientPhoneNumber,
        string RecipientEmail);