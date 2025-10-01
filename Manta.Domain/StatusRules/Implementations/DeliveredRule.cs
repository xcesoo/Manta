using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class DeliveredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch //todo check context as switch
        {
            {Parcel: {CurrentStatus: { Status: 
                        EParcelStatus.ReadyForPickup}, 
                    Paid: true, 
                    InRightLocation:true}} => 
                RuleResult.Ok(EParcelStatus.Delivered),
            
            {Parcel:{CurrentStatus:{Status:
                EParcelStatus.ReadyForPickup}, 
                InRightLocation: false}}=>
                RuleResult.Failed(ERuleResultError.LocationMismatch, "Parcel is not currently located at right delivery point."),
            
            {Parcel: {CurrentStatus: {Status:
                        EParcelStatus.ReadyForPickup}, 
                    Paid: false}} =>
                RuleResult.Failed(ERuleResultError.PaymentRequired, "Cannot deliver a parcel: amount is still due."),
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Cannot deliver a parcel. Parcel -> {context.Parcel.CurrentStatus.Status}")

        };
    
}