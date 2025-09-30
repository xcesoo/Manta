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

    public void DeliverParcel(Parcel parcel, User changeBy, DeliveryPoint deliveryPoint)
    {
        var context = RuleContext.ForDelivery(parcel, changeBy, deliveryPoint);
        if (_statusService.ApplyRule<DeliveredRule>(context))
        {
            DomainEvents.Raise(new ParcelDeliveredEvent(parcel, deliveryPoint, changeBy));
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
        if (_statusService.ApplyRule<ReaddressRequestedRule>(context))
        {
            parcel.Readdress(deliveryPoint.Id);
            //TODO raise event
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

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
        parcel.Cancel(cancelledBy);
        //TODO raise event + rule
    }
}