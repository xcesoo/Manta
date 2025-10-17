using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;

namespace Manta.Application.Services;

public static class DeliveryPointFactory
{
    public static async Task Create(DeliveryPointCreationOptions options, IDeliveryPointRepository context)
    {
        var newOptions = options with { Id = await context.GetNextIdAsync() };
        var deliveryPoint = DeliveryPoint.Create(newOptions);
        await context.AddAsync(deliveryPoint);
        await context.SaveChangesAsync();
    }
}