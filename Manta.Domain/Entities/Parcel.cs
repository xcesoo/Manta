using Manta.Domain.Enums;

namespace Manta.Domain.Entities;

public class Parcel
{
    public int Id {get; private set;}
    public int DeliveryPointId {get; private set;}
    private readonly List<ParcelStatus> _history = new();
    public IEnumerable<ParcelStatus> StatusHistory => _history.AsReadOnly();

    public ParcelStatus CurrentStatus => _history[^1];
    
    private Parcel(int id, int deliveryPointId)
    {
        Id=id;
        DeliveryPointId=deliveryPointId;
    }

    internal static Parcel Create(int id, int deliveryPointId, User createdBy)
    {
        if(id<=0) 
            throw new ArgumentOutOfRangeException(nameof(id) + "Id can't be null");
        if(deliveryPointId<=0) 
            throw new ArgumentOutOfRangeException(nameof(deliveryPointId) + "DeliveryPointId can't be null");
        var parcel = new Parcel(id, deliveryPointId);
        parcel.ChangeStatus(EParcelStatus.Processing, createdBy);
        return parcel;
    }
    
        
    public void ChangeStatus(EParcelStatus newStatus, User? changedBy)
    {
        _history.Add(new ParcelStatus(newStatus, changedBy ?? SystemUser.Instance));
    }
}