using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        return parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ReadyForPickup when parcel is { Paid: true, InRightLocation: true } => 
                RuleResult.Ok(EParcelStatus.Delivered),
            
            EParcelStatus.ReadyForPickup when parcel is {InRightLocation: false} =>
                RuleResult.Failed(ERuleResultError.LocationMismatch, "Parcel is not currently located at this delivery point."),
            
            EParcelStatus.ReadyForPickup when parcel is {Paid: false} =>
                RuleResult.Failed(ERuleResultError.PaymentRequired, "Cannot deliver a parcel: amount is still due."),
            
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Cannot deliver a parcel. Parcel -> {parcel.CurrentStatus.Status}")
        };
    }
}