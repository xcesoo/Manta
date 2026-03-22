using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;

namespace Manta.Application.Factories;

public static class DeliveryPointFactory
{
    public static async Task<DeliveryPoint> Create(DeliveryPointCreationOptions options)
    {
        return DeliveryPoint.Create(options);
    }
}