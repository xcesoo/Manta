using Manta.Domain.Entities;

namespace Manta.Application.Services;

public static class DeliveryPointFactory
{
    public static DeliveryPoint Create(int id, string address) => DeliveryPoint.Create(id, address);
}