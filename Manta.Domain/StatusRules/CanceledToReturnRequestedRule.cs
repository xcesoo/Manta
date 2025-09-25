using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Exceptions;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class CanceledToReturnRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        if (parcel.CurrentStatus.Status == EParcelStatus.Delivered)
            return RuleResult.Failed("PAD", "Parcel is already delivered");
        if (parcel.CurrentStatus.Status == EParcelStatus.ShipmentCancelled)
        {
            return RuleResult.Ok(EParcelStatus.ReturnRequested);
        }
        return RuleResult.Failed("PAD", "Parcel is not in the right status"); //todo
    }
}