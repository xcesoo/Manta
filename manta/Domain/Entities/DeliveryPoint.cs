using System.Data;
using manta.Domain.Enums;
using manta.Domain.Services;

namespace manta.Domain.Entities;

public class DeliveryPoint
{
    public int Id { get; private set; }
    public string Address { get; private set; }
    private readonly List<Parcel> _parcels = new();
    private readonly ParcelStatusService _statusService = new();

    public IEnumerable<Parcel> Parcels => _parcels.AsReadOnly();

    public DeliveryPoint(int id, string address)
    {
        Id = id;
        Address = address;
    }

    public void AddParcel(Parcel parcel, User changedBy)
    {
        if (parcel == null)
            throw new ArgumentNullException(nameof(parcel));
        _parcels.Add(parcel);
        _statusService.UpdateStatus(parcel, changedBy, this);
    }
    public void PrintInfo()
    {
        Console.WriteLine($"Delivery Point Id: {Id}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Parcels count: {_parcels.Count}");
    
        if (_parcels.Count == 0)
        {
            Console.WriteLine("No parcels in this delivery point.");
            return;
        }

        Console.WriteLine("Parcels:");
        foreach (var parcel in _parcels)
        {
            Console.WriteLine($"  Parcel Id: {parcel.Id}");
            Console.WriteLine($"  Current Status: {parcel.CurrentStatus}");
            Console.WriteLine($"  Status history:");
            foreach (var s in parcel.StatusHistory) // якщо Statuses зроблені public або internal для доступу
            {
                Console.WriteLine($"    - {s.Status} at {s.ChangedAt} by {(s.ChangedBy?.Email ?? "System")}");
            }
            Console.WriteLine();
        }
    }

}