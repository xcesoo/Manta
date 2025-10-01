using System.Data;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public sealed class WrongDeliveryPointRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {DeliveryPoint: null} => RuleResult.Failed(ERuleResultError.ArgumentInvalid, "DeliveryPoint is null"),
        
        {Parcel:{CurrentStatus:{Status: 
            EParcelStatus.Processing or 
            EParcelStatus.InTransit or 
            EParcelStatus.ReaddressRequested}, 
            DeliveryPointId: var id}, DeliveryPoint: {Id: var dp}} when dp != id => 
            RuleResult.Ok(EParcelStatus.WrongLocation),
        
        _ =>
            RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Cannot apply Wrong DeliveryPoint. Parcel -> {context.Parcel.CurrentStatus.Status}")
    };
}