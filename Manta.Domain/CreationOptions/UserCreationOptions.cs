using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record UserCreationOptions(
    Guid Id,
    Name Name,
    Email Email,
    string PasswordHash,
    Guid? DeliveryPointId = null,
    LicensePlate? VehicleId = null
    );