using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public sealed class ReadyForPickupRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {DeliveryPoint: null} => 
            RuleResult.Failed(
                ERuleResultError.ArgumentInvalid, 
                "DeliveryPoint is null"),
        
        {Parcel: {CurrentStatus: {Status: 
            EParcelStatus.Processing or 
            EParcelStatus.InTransit or 
            EParcelStatus.WrongLocation or 
            EParcelStatus.ReaddressRequested}, 
                DeliveryPointId: var dp}, DeliveryPoint: {Id: var id}} when dp == id => 
            RuleResult.Ok(EParcelStatus.ReadyForPickup),
        
        _ => RuleResult.Failed(
            ERuleResultError.Unknown, 
            $"Failed to pick up a parcel. Parcel -> {context.Parcel.CurrentStatus.Status}")
    };
}