using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        if (parcel.CurrentLocationDeliveryPointId != deliveryPoint.Id)
            return RuleResult.Failed(
                ERuleResultError.LocationMismatch, 
                "Parcel is not currently located at this delivery point.");
        if (!parcel.Paid)
            return RuleResult.Failed(
                ERuleResultError.PaymentRequired,
                "Cannot deliver a parcel: amount is still due.");
        
        return parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ReadyForPickup => RuleResult.Ok(EParcelStatus.Delivered),
            
            EParcelStatus.Delivered or EParcelStatus.PartiallyReceived =>
                RuleResult.Failed(
                    ERuleResultError.ParcelAlreadyDelivered, 
                    "Parcel has already been delivered or partially received."),
            
            EParcelStatus.ShipmentCancelled =>
                RuleResult.Failed(
                    ERuleResultError.ParcelWrongStatus,
                    "Parcel has been canceled."),
            
            EParcelStatus.ReturnRequested or 
                EParcelStatus.ReturnProcessed or
                EParcelStatus.ReturnedToContainer or
                EParcelStatus.ReturnGivenToCourier or
                EParcelStatus.Returned =>
                RuleResult.Failed(
                    ERuleResultError.ParcelWrongStatus,
                    "Parcel is currently involved in a return process"),
            
            _ => RuleResult.Failed(
                ERuleResultError.Unknown, 
                "Cannot deliver a parcel.")
        };
    }
}