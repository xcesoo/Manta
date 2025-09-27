using System.Data;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class WrongDeliveryPointRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        return parcel.CurrentStatus.Status switch
        {
            EParcelStatus.Processing or
                EParcelStatus.InTransit when 
                parcel.DeliveryPointId != deliveryPoint.Id =>
                RuleResult.Ok(EParcelStatus.WrongLocation),
            
            _ => RuleResult.Failed(
                ERuleResultError.LocationMismatch, 
                "Parcel in right location")
        };
    }
}