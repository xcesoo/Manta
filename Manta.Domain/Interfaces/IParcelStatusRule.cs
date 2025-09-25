using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Interfaces;

public interface IParcelStatusRule
{
    RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint); //todo pattern matching for rules (switch)
}