using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;

namespace Manta.Application.Factories;

public static class DeliveryVehicleFactory
{
    public static DeliveryVehicle Create(DeliveryVehicleCreationOptions options) =>
        DeliveryVehicle.Create(options);
}