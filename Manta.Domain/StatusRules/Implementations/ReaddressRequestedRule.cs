using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class ReaddressRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        { Parcel: { CurrentStatus: { Status: 
                EParcelStatus.Delivered or 
                EParcelStatus.PartiallyReceived } } }
            => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                "Cannot readdress a delivered/partially received parcel."),

        { Parcel: { CurrentStatus: { Status: 
                EParcelStatus.ReturnRequested or 
                EParcelStatus.InReturnTransit or 
                EParcelStatus.Returned or 
                EParcelStatus.ShipmentCancelled } } }
            => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                "Parcel is involved in a return process."),
        
        { Parcel: { CurrentStatus: { Status: EParcelStatus.WrongLocation } }, DeliveryPoint: null }
            => RuleResult.Ok(EParcelStatus.ReaddressRequested),
        
        { DeliveryPoint: null }
            => RuleResult.Failed(
                ERuleResultError.ArgumentInvalid, 
                "Target delivery point is required."),
        
        { Parcel: { DeliveryPointId: var dest }, DeliveryPoint: { Id: var id } } when dest == id
            => RuleResult.Failed(
                ERuleResultError.LocationMismatch, 
                "Target equals parcel destination."),
        
        { Parcel: { CurrentLocationDeliveryPointId: var cur }, DeliveryPoint: { Id: var id } } when cur == id
            => RuleResult.Failed(ERuleResultError.LocationMismatch, "Target equals current location."),
        
        _ => RuleResult.Ok(EParcelStatus.ReaddressRequested)
    };
}