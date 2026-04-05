using Manta.Domain.CreationOptions;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public class Parcel
{
    public Guid Id { get; private set; }
    public Guid DeliveryPointId {get; private set;}
    public Guid? CurrentLocationDeliveryPointId { get; private set; }
    public Guid? CurrentVehicleId { get; private set; } // Визначає в якому траспортному засобі знаходиться посилка
    public bool InRightLocation => DeliveryPointId == CurrentLocationDeliveryPointId; // Визначає чи посилка знаходиться у пункті призначення
    public decimal AmountDue { get; private set; } // Сума до сплати
    public bool Paid => AmountDue == 0; // Визначає, чи сплачено
    public double Weight { get; private set; } // Вага посилки
    
    public Name RecipientName { get; private set; } // Ім’я отримувача як об’єкт-значення
    public PhoneNumber RecipientPhoneNumber { get; private set; } // Номер телефона отримувача як об’єкт-значення
    public Email RecipientEmail { get; private set; } // Email отримувача як об’єкт-значення
    
    public virtual ICollection<ParcelStatus> StatusHistory { get; private set; } = new List<ParcelStatus>();
    public ParcelStatus CurrentStatus => StatusHistory.Last();
    public DateTime? ArrivedAt { get; private set; } = null;
    public DateTime? Storage => ArrivedAt?.ToLocalTime() + TimeSpan.FromDays(3);
    private Parcel() { }
    private Parcel(Guid id, Guid deliveryPointId, Name recipientName, PhoneNumber recipientPhoneNumber, Email recipientEmail, double weight, decimal amountDue)
    {
        Id = id;
        DeliveryPointId=deliveryPointId;
        RecipientName = recipientName;
        RecipientPhoneNumber = recipientPhoneNumber;
        RecipientEmail = recipientEmail;
        Weight = weight;
        CurrentLocationDeliveryPointId = null;
        AmountDue = amountDue;
    }

    internal static Parcel Create(ParcelCreationOptions options)
    {
        var parcel = new Parcel(
            options.Id,
            options.DeliveryPointId,
            options.RecipientName,
            options.RecipientPhoneNumber,
            options.RecipientEmail,
            options.Weight,
            options.AmountDue);
        
        parcel.ChangeStatus(EParcelStatus.Processing, options.CreatedBy);
        return parcel;
    }

    internal void ChangeStatus(EParcelStatus newStatus, UserInfo changedBy) =>
        StatusHistory.Add(new ParcelStatus(newStatus, changedBy));
    
    internal void Pay() => AmountDue = 0m;
    
    internal void ChangeRecipientName(Name newName) => RecipientName = newName;
    internal void ChangeRecipientPhoneNumber(PhoneNumber newPhoneNumber) => RecipientPhoneNumber = newPhoneNumber;
    internal void ChangeRecipientEmail(Email newEmail) => RecipientEmail = newEmail;
    internal void ChangeRecipientName(string newName) => ChangeRecipientName(new Name(newName));
    internal void ChangeRecipientPhoneNumber(string newPhoneNumber) => ChangeRecipientPhoneNumber(new PhoneNumber(newPhoneNumber));
    internal void ChangeRecipientEmail(string newEmail) => ChangeRecipientEmail(new Email(newEmail));
    
    internal void ChangeAmountDue(decimal newAmountDue) => AmountDue = newAmountDue;
    
    internal void ChangeWeight(double newWeight) => Weight = newWeight;

    internal void Readdress(Guid newDeliveryPointId) => DeliveryPointId = newDeliveryPointId;
    internal void  MoveToLocation(Guid? newCurrentLocationDeliveryPointId) => CurrentLocationDeliveryPointId = newCurrentLocationDeliveryPointId;

    internal void ChangeDeliveryVehicle(Guid? newVehicleId) => CurrentVehicleId = newVehicleId;
    internal void Cancel(UserInfo cancelledBy)
    {
        ChangeStatus(EParcelStatus.ShipmentCancelled, cancelledBy);
    }
    internal void ChangeArrivedAt(DateTime arrivedAt) => ArrivedAt = arrivedAt;
}
