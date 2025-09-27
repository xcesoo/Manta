using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReaddressRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        return parcel.CurrentStatus.Status switch
        {
            _ when parcel.CurrentLocationDeliveryPointId == deliveryPoint.Id => 
                RuleResult.Failed(
                ERuleResultError.LocationMismatch, 
                $"Cannot to readdress a parcel to the same delivery point"),
            
            EParcelStatus.Delivered or 
                EParcelStatus.PartiallyReceived => 
                RuleResult.Failed(
                    ERuleResultError.WrongParcelStatus, 
                    "Cannot to readdress a delivered parcel"),
            
            EParcelStatus.ReturnRequested or 
                EParcelStatus.InReturnTransit or 
                EParcelStatus.Returned =>
                RuleResult.Failed(
                    ERuleResultError.WrongParcelStatus, 
                    "Parcel is currently involved in a return process"),
            
            _ => RuleResult.Ok(EParcelStatus.ReaddressRequested)
        };
    }
}