using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;

namespace Manta.Application.Factories;

public static class DeliveryVehicleFactory
{
    public static async Task<DeliveryVehicle> Create(DeliveryVehicleCreationOptions options)
    {
        var vehicle = DeliveryVehicle.Create(options);
        return vehicle;
    }
}