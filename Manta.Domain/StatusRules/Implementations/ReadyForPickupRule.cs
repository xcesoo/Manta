using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Context;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class ReadyForPickupRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {DeliveryPoint: null} => 
            RuleResult.Failed(
                ERuleResultError.ArgumentInvalid, 
                "DeliveryPoint is null"),
        
            // TWIST A: 
        {DeliveryPoint: var dp, ActiveParcelsCount: var count} when count >= dp.Capacity =>
        RuleResult.Failed(ERuleResultError.SlotUnavailable, $"Capacity limit exceeded. Max: {dp.Capacity}"),
        
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