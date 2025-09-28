using Manta.Domain.CreationOptions;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public class DeliveryVehicle
{
    public LicensePlate Id { get; private set; }
    public CarModel CarModel { get; private set; }
    public double Capacity { get; private set; }
    public double CurrentLoad { get; private set; } = 0;
    
    private readonly List<int> _parcelsIds = new List<int>();
    public IEnumerable<int> ParcelsIds => _parcelsIds.AsReadOnly();
    
    private DeliveryVehicle(LicensePlate id, CarModel carModel, double capacity)
    {
        Id = id;
        CarModel = carModel;
        Capacity = capacity;
    }

    internal static DeliveryVehicle Create(DeliveryVehicleCreationOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), $"{nameof(options)} cannot be null.");
        if (options.Capacity <= 0)
            throw new ArgumentException("The capacity must be greater than zero.", nameof(options.Capacity));
        return new DeliveryVehicle(
            options.LicensePlate,
            options.CarModel,
            options.Capacity);
    }

    internal void LoadParcel(int parcelId, double parcelWeight)
    {
        if(parcelWeight + CurrentLoad > Capacity)
            throw new ArgumentException("Delivery vehicle is full.");
        if(_parcelsIds.Contains(parcelId))
            throw new ArgumentException($"The parcel with id {parcelId} is already loaded in the delivery vehicle {Id}.");
        _parcelsIds.Add(parcelId);
        CurrentLoad += parcelWeight;
    }

    internal void UnloadParcel(int parcelId, double parcelWeight)
    {
        if (!_parcelsIds.Contains(parcelId))
            throw new ArgumentException($"The parcel with ID {parcelId} does not exist in the delivery vehicle {Id}.");
        _parcelsIds.Remove(parcelId);
        CurrentLoad -= parcelWeight;
    }
}