using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReaddressRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context)
    {
        if (context.DeliveryPoint is null)
            return RuleResult.Failed(ERuleResultError.ArguementInvalid, "DeliveryPoint is null");
        
        return context.Parcel.CurrentStatus.Status switch
        {
            _ when context.Parcel.CurrentLocationDeliveryPointId == context.DeliveryPoint.Id => 
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