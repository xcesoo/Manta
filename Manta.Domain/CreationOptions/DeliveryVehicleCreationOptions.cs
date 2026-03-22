using Manta.Domain.ValueObjects;

namespace Manta.Domain.CreationOptions;

public record DeliveryVehicleCreationOptions(
    int Id,
    LicensePlate LicensePlate,
    CarModel CarModel,
    double Capacity
    );