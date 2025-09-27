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
        return parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ShipmentCancelled =>
                RuleResult.Ok(EParcelStatus.ReturnRequested),
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Unable to return request. Parcel -> {parcel.CurrentStatus.Status}")
        };
    }
}