namespace Manta.Domain.Entities;

public record ParcelCreationOptions (
    int Id,
    int DeliveryPointId,
    decimal AmountDue,
    double Weight,
    Name? RecipientName,
    PhoneNumber? RecipientPhoneNumber,
    Email? RecipientEmail,
    User? CreatedBy
);