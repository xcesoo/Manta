using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record DeliveryVehicleCreationOptions(
    Guid Id,
    LicensePlate LicensePlate,
    CarModel CarModel,
    double Capacity
    );