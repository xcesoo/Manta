using Manta.Domain.Entities;
using Manta.Domain.Services;

namespace Manta.Application.Services;

public static class ParcelFactory
{
    public static Parcel Create(int id, int deliveryPointId, User createdBy) =>
        Parcel.Create(id, deliveryPointId, createdBy);
}