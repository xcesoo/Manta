using manta.Domain.Enums;

namespace manta.Domain.Entities;

public class Parcel
{
    public int Id { get; private set; } // Унікальний id посилки
    public int DeliveryPointId { get; private set; } // id Відділення доставки 
    public IReadOnlyCollection<ParcelStatus> StatusHistory => 
        _statusHistory.AsReadOnly(); // Історія всіх статусів, інкапсульований
    public ParcelStatus? CurrentStatus => 
        _statusHistory.Count == 0 ? null : _statusHistory[^1]; // Отримання поточного статусу посилки (останній запис в list)
    
    private List<ParcelStatus> _statusHistory = new(); // Історія посилок доступна до змін
    

    public Parcel(int deliveryPointId)
    {
        Id = 1;
        DeliveryPointId = deliveryPointId;
        ChangeStatus(EParcelStatus.Processing, new Admin(1, "root", "root"));
    }

    public void ChangeStatus(EParcelStatus status, User changedBy) =>
    _statusHistory.Add(new ParcelStatus(status, changedBy));

    public void PrintInfo()
    {
        Console.WriteLine($"Parcel Id: {Id}");
        Console.WriteLine($"Delivery Point Id: {DeliveryPointId}");
        Console.WriteLine($"Current Status: {CurrentStatus}");
        Console.WriteLine($"Statuses count: {_statusHistory.Count}");
        foreach (var s in _statusHistory)
            Console.WriteLine($"{s.Status} by {s.ChangedBy?.Email} at {s.ChangedAt}");
    }
}