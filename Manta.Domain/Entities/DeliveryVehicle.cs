using Manta.Domain.CreationOptions;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public class DeliveryVehicle
{
    public Guid Id { get; private set; } 
    public LicensePlate LicensePlateId { get; private set; }
    public CarModel CarModel { get; private set; }
    public double Capacity { get; private set; }
    public double CurrentLoad { get; private set; } = 0;
    
    public virtual ICollection<Guid> ParcelsIds { get; private set; } = new List<Guid>();
    
    private DeliveryVehicle() { }

    private DeliveryVehicle(Guid id, LicensePlate licensePlateId, CarModel carModel, double capacity)
    {
        Id = id;
        LicensePlateId = licensePlateId;
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
            options.Id,
            options.LicensePlate,
            options.CarModel,
            options.Capacity);
    }

    internal void LoadParcel(Guid parcelId, double parcelWeight)
    {
        if(parcelWeight + CurrentLoad > Capacity)
            throw new ArgumentException("Delivery vehicle is full.");
        if(ParcelsIds.Contains(parcelId))
            throw new ArgumentException($"The parcel with id {parcelId} is already loaded in the delivery vehicle {LicensePlateId}.");
        ParcelsIds.Add(parcelId);
        CurrentLoad += parcelWeight;
    }

    internal void UnloadParcel(Guid parcelId, double parcelWeight)
    {
        if (!ParcelsIds.Contains(parcelId))
            throw new ArgumentException($"The parcel with ID {parcelId} does not exist in the delivery vehicle {LicensePlateId}.");
        ParcelsIds.Remove(parcelId);
        CurrentLoad -= parcelWeight;
    }
}