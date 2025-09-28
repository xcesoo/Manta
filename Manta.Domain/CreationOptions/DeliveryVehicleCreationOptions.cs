using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record DeliveryVehicleCreationOptions(
    LicensePlate LicensePlate,
    CarModel CarModel,
    double Capacity
    );