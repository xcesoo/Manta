using System.Data;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class WrongDeliveryPointRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context)
    {
        if (context.DeliveryPoint is null)
            return RuleResult.Failed(ERuleResultError.ArguementInvalid, "DeliveryPoint is null");
        
        return context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.Processing or
                EParcelStatus.InTransit when 
                context.Parcel.DeliveryPointId != context.DeliveryPoint.Id =>
                RuleResult.Ok(EParcelStatus.WrongLocation),
            
            _ when context.Parcel.DeliveryPointId == context.DeliveryPoint.Id =>
                RuleResult.Failed(
                ERuleResultError.LocationMismatch, 
                "Parcel in right location"),
            
            _ =>
                RuleResult.Failed(
                    ERuleResultError.WrongParcelStatus, 
                    $"Cannot apply Wrong DeliveryPoint. Parcel -> {context.Parcel.CurrentStatus.Status}")
        };
    }
}