using manta.Domain.Enums;
using manta.Domain.Events;
using manta.Domain.Services;
using manta.Domain.StatusRules;
using manta.Infrastructure.EventDispatcher;

namespace manta.Domain.Entities;

public class DeliveryPoint
{
    public int Id { get; private set; }
    public string Address { get; private set; }
    private readonly List<Parcel> _parcels = new();
    private readonly ParcelStatusService _statusService;

    public IEnumerable<Parcel> Parcels => _parcels.AsReadOnly();

    public DeliveryPoint(int id, string address, ParcelStatusService statusService)
    {
        Id = id;
        Address = address;
        _statusService = statusService;
    }

    public void AddParcel(Parcel parcel, User changedBy)
    {
        if (parcel == null)
            throw new ArgumentNullException(nameof(parcel));
        _parcels.Add(parcel);
        DomainEvents.Raise(new ParcelAddedToDeliveryPointEvent(parcel, this, changedBy));
    }

    internal void RemoveParcel(Parcel parcel)
    {
        if(parcel == null) 
            throw new ArgumentNullException(nameof(parcel));
        _parcels.Remove(parcel);
    }

    public void DeliveryParcel(Parcel parcel, User changedBy)
    {
        if (!_parcels.Contains(parcel))
            throw new ArgumentException("Parcel does not exist", nameof(parcel));
        DomainEvents.Raise(new ParcelDeliveredEvent(parcel, this, changedBy));
    }
    
}