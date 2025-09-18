using System.Runtime.CompilerServices;
using Manta.Domain.Events;
using Manta.Domain.Services;
[assembly: InternalsVisibleTo("Manta.Application")]

namespace Manta.Domain.Entities;
public class DeliveryPoint
{
    public int Id { get; private set; }
    public string Address { get; private set; }
    private readonly List<Parcel> _parcels = new();

    public IEnumerable<Parcel> Parcels => _parcels.AsReadOnly();

    private DeliveryPoint(int id, string address)
    {
        Id = id;
        Address = address;
    }

    internal static DeliveryPoint Create(int id, string address)
    {
        if (id <= 0) throw new ArgumentException("Id can't be null", nameof(id));
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address can't be null", nameof(address));
        return new DeliveryPoint(id, address);
    }

    public bool ContainsParcel(Parcel parcel) => _parcels.Contains(parcel);

    internal void AddParcel(Parcel parcel)
    {
        if (parcel == null)
            throw new ArgumentNullException(nameof(parcel));
        if (_parcels.Contains(parcel))
            throw new InvalidOperationException("Parcel already exists in this delivery point.");
        _parcels.Add(parcel); ;
    }

    internal void RemoveParcel(Parcel parcel)
    {
        if(parcel == null) 
            throw new ArgumentNullException(nameof(parcel) + "Parcel can't be null");
        _parcels.Remove(parcel);
    }
    
    
}