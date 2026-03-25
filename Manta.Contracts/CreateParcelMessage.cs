namespace Manta.Contracts;

public record CreateParcelMessage
    (
        Guid Id,
        Guid DeliveryPointId,
        Guid CreatedById,
        decimal AmountDue,
        double Weight,
        string RecipientName,
        string RecipientPhoneNumber,
        string RecipientEmail);