using Manta.Domain.Entities;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record ParcelCreationOptions (
    int Id,
    int DeliveryPointId,
    decimal AmountDue,
    double Weight,
    Name RecipientName,
    PhoneNumber RecipientPhoneNumber,
    Email RecipientEmail,
    User CreatedBy
);