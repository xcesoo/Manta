using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Domain.Interfaces;

public interface IParcelStatusRule
{
    bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus);
}