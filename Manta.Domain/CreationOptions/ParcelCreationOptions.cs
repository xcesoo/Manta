using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record ParcelCreationOptions (
    Guid Id,
    Guid DeliveryPointId,
    decimal AmountDue,
    double Weight,
    Name RecipientName,
    PhoneNumber RecipientPhoneNumber,
    Email RecipientEmail,
    UserInfo CreatedBy);