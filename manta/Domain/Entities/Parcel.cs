using manta.Domain.Enums;

namespace manta.Domain.Entities;

public class Parcel
{
    public int Id { get; private set; }
    public int DeliveryPointId { get; private set; }
    private List<ParcelStatus> Statuses { get; set; }
    public ParcelStatus Status { get; private set; }

    public Parcel(int deliveryPointId)
    {
        Id = 1;
        DeliveryPointId = deliveryPointId;
        Status = new ParcelStatus(EParcelStatus.Processing, new User("popa"));
        Statuses = new List<ParcelStatus>();
        Statuses.Add(Status);
    }

    public void ChangeStatus(ParcelStatus status)
    {
        Status = status;
        Statuses.Add(Status);
    }

    public void GetInfo()
    {
        Console.WriteLine($"Parcel Id: {Id}");
        Console.WriteLine($"Delivery Point Id: {DeliveryPointId}");
        Console.WriteLine($"Status: {Status.ToString()}"); 
        Console.WriteLine($"Statuses count: {Statuses.Count}");
    }
}