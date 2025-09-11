using manta.Domain.Entities;
using manta.Domain.Enums;
namespace manta.Domain.Interfaces;

public interface IParcelStatusRule
{
    bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus);
}