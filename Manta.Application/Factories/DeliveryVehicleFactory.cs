using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;

namespace Manta.Application.Factories;

public static class DeliveryVehicleFactory
{
    public static async Task Create(DeliveryVehicleCreationOptions options, IDeliveryVehicleRepository context)
    {
        var vehicle = DeliveryVehicle.Create(options);
        await context.AddAsync(vehicle);
        await context.SaveChangesAsync();
    }
}