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
        CurrentLoad += parcelWeight;
    }

    internal void UnloadParcel(Guid parcelId, double parcelWeight)
    {
        CurrentLoad -= parcelWeight;
    }
}