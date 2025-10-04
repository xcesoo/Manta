using Manta.Domain.Entities;
using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Domain.StatusRules;
using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Policies;
using Manta.Infrastructure.EventDispatcher;

namespace Manta.Application.Services;

public class ParcelDeliveryService
{
    private readonly ParcelStatusService _statusService;
    public ParcelDeliveryService(ParcelStatusService statusService)
    {
        _statusService = statusService;
    }

    public void DeliverParcel(Parcel parcel, User changeBy)
    {
        var context = RuleContext.ForParcel(parcel, changeBy);
        if (_statusService.ApplyRule<DeliveredRule>(context))
        {
            DomainEvents.Raise(new ParcelDeliveredEvent(parcel, changeBy));
        }
        else throw new ArgumentException($"Failed to deliver the parcel {nameof(parcel)}");
    }

    public void AcceptedAtDeliveryPoint(DeliveryPoint deliveryPoint, Parcel parcel, User changedBy)
    {
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (_statusService.ApplyRule<AcceptAtDeliveryPointPolicy>(context))
        {
            parcel.MoveToLocation(deliveryPoint.Id);
            DomainEvents.Raise(new ParcelAddedToDeliveryPointEvent(parcel, deliveryPoint, changedBy));
        }
        else throw new ArgumentException("Failed to accept the parcel", nameof(parcel));
    }

    public void ReaddressParcel(DeliveryPoint deliveryPoint, Parcel parcel, User changedBy)
    {
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(deliveryPoint.Id);
            //TODO raise event
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

    public void ReaddressParcel(Parcel parcel, User changedBy)
    {
        var context = RuleContext.ForParcel(parcel, changedBy);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(parcel.DeliveryPointId);
            //todo raise event
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

    private bool CanReaddressParcel(RuleContext context) => _statusService.ApplyRule<ReaddressRequestedRule>(context);

    public void LoadInDeliveryVehicle(DeliveryVehicle deliveryVehicle, Parcel parcel, User changeBy)
    {
        deliveryVehicle.LoadParcel(parcel.Id, parcel.Weight);
        //todo
    }

    public void UnloadFromDeliveryVehicle(DeliveryVehicle deliveryVehicle, Parcel parcel, User changeBy)
    {
        deliveryVehicle.UnloadParcel(parcel.Id, parcel.Weight);
    }
    public void CancelParcel(Parcel parcel, User cancelledBy)
    {
        var context = RuleContext.ForParcel(parcel, cancelledBy);
        if (_statusService.ApplyRule<ShipmentCancelledRule>(context))
        {
        //TODO raise event + rule
        }
    }
}