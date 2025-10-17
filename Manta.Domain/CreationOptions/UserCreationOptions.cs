using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record UserCreationOptions(
    Name Name,
    Email Email,
    int? DeliveryPointId = null,
    LicensePlate? VehicleId = null,
    int? Id = null
    );