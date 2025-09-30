using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public class DeliveredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context)
    {
        return context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ReadyForPickup when context.Parcel is
                {
                    Paid: true, 
                    InRightLocation: true
                } => 
                RuleResult.Ok(EParcelStatus.Delivered),
            
            EParcelStatus.ReadyForPickup when context.Parcel is
                {
                    InRightLocation: false
                } =>
                RuleResult.Failed(ERuleResultError.LocationMismatch, "Parcel is not currently located at right delivery point."),
            
            EParcelStatus.ReadyForPickup when context.Parcel is {Paid: false} =>
                RuleResult.Failed(ERuleResultError.PaymentRequired, "Cannot deliver a parcel: amount is still due."),
            
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Cannot deliver a parcel. Parcel -> {context.Parcel.CurrentStatus.Status}")
        };
    }
}