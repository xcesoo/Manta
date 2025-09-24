using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public class Parcel
{
    public int Id {get; private set;}
    public int DeliveryPointId {get; private set;}
    public int? CurrentLocationDeliveryPointId { get; private set; }
    public decimal AmountDue { get; private set; }
    public bool Paid => AmountDue == 0;
    public double Weight { get; private set; }
    
    public Name RecipientName { get; private set; }
    public PhoneNumber RecipientPhoneNumber { get; private set; }
    public Email RecipientEmail { get; private set; }
    
    private readonly List<ParcelStatus> _history = new List<ParcelStatus>();
    public IEnumerable<ParcelStatus> StatusHistory => _history.AsReadOnly();
    public ParcelStatus CurrentStatus => _history[^1];
    
    private Parcel(int id, int deliveryPointId, Name recipientName, PhoneNumber recipientPhoneNumber, Email recipientEmail, double weight)
    {
        Id=id;
        DeliveryPointId=deliveryPointId;
        RecipientName = recipientName;
        RecipientPhoneNumber = recipientPhoneNumber;
        RecipientEmail = recipientEmail;
        Weight = weight;
        CurrentLocationDeliveryPointId = null;
    }

    internal static Parcel Create(ParcelCreationOptions options)
    {
        if(options.Id<=0) 
            throw new ArgumentOutOfRangeException(nameof(options.Id) + "Id can't be null");
        if(options.DeliveryPointId<=0) 
            throw new ArgumentOutOfRangeException(nameof(options.DeliveryPointId) + "DeliveryPointId can't be null");
        
        var parcel = new Parcel(
            options.Id,
            options.DeliveryPointId,
            options.RecipientName,
            options.RecipientPhoneNumber,
            options.RecipientEmail,
            options.Weight);
        
        parcel.ChangeStatus(EParcelStatus.Processing, options.CreatedBy);
        return parcel;
    }


    internal void ChangeStatus(EParcelStatus newStatus, User? changedBy) =>
        _history.Add(new ParcelStatus(newStatus, changedBy ?? SystemUser.Instance));
    
    internal void ChangeRecipientName(Name newName) => RecipientName = newName;
    internal void ChangeRecipientPhoneNumber(PhoneNumber newPhoneNumber) => RecipientPhoneNumber = newPhoneNumber;
    internal void ChangeRecipientEmail(Email newEmail) => RecipientEmail = newEmail;
    internal void ChangeRecipientName(string newName) => ChangeRecipientName(new Name(newName));
    internal void ChangeRecipientPhoneNumber(string newPhoneNumber) => ChangeRecipientPhoneNumber(new PhoneNumber(newPhoneNumber));
    internal void ChangeRecipientEmail(string newEmail) => ChangeRecipientEmail(new Email(newEmail));
    
    internal void ChangeAmountDue(decimal newAmountDue) => AmountDue = newAmountDue;
    
    internal void ChangeWeight(double newWeight) => Weight = newWeight;

    internal void Readdress(int newDeliveryPointId) => DeliveryPointId = newDeliveryPointId;
    internal void MoveToLocation(int? newCurrentLocationDeliveryPointId) => CurrentLocationDeliveryPointId = newCurrentLocationDeliveryPointId;

    internal void Cancel(User cancelledBy)
    {
        if(CurrentStatus.Status == EParcelStatus.Delivered)
            throw new InvalidOperationException("Delivered parcel can't be cancelled");
        
        ChangeStatus(EParcelStatus.ShipmentCancelled, cancelledBy);
    }
}