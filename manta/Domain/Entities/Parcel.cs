using manta.Domain.Enums;

namespace manta.Domain.Entities;

public class Parcel
{
    //Ідентичність та базові поля
    public int Id {get; private set;}
    public int DeliveryPointId {get; private set;}
    //Привітна історія статусів до змін
    private readonly List<ParcelStatus> _history = new();
    public IReadOnlyList<ParcelStatus> StatusHistory => _history.AsReadOnly();
    //Лямбда функція поточного статусу посилки
    public ParcelStatus CurrentStatus => _history[^1];

    //Конструктор
    private Parcel(int id, int deliveryPointId)
    {
        Id=id;
        DeliveryPointId=deliveryPointId;
    }

    public static Parcel Create(int id, int deliveryPointId)
    {
        if(id<=0) throw new ArgumentOutOfRangeException(nameof(id));
        if(deliveryPointId<=0) throw new ArgumentOutOfRangeException(nameof(deliveryPointId));
        
        var parcel = new Parcel(id, deliveryPointId);
        //Задаємо початковий статус
        parcel._history.Add(new ParcelStatus(EParcelStatus.Processing,SystemUser.Instance));
        return parcel;
    }
    
        
    public void ChangeStatus(EParcelStatus newStatus, User? changedBy)
    {
        _history.Add(new ParcelStatus(newStatus, changedBy ?? SystemUser.Instance));
    }
}