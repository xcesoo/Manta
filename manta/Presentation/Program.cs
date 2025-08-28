namespace manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
        var dp = new DeliveryPoint(10149, "м. Тарасівка, вул. Шевченка 2/6", "@10149tg");
        var parcel1 = new Parcel(1,10149);
        var parcel2 = new Parcel(2,10148);
        Console.WriteLine($"parcel_id_{parcel1.Id} is {parcel1.Status}");
        Console.WriteLine($"parcel_id_{parcel2.Id} is {parcel2.Status}");
        dp.AddParcel(parcel1);
        dp.AddParcel(parcel2);
        Console.WriteLine($"parcel_id_{parcel1.Id} is {parcel1.Status}");
        Console.WriteLine($"parcel_id_{parcel2.Id} is {parcel2.Status}");
    }
}
public class DeliveryPoint
{
    public int Id {get; private set;}
    public string Address {get; private set;}
    public string TelegramAccount {get; private set;}
    public bool IsActive {get; private set;}
    
    private readonly List<Parcel> _parcels = new();
    public IReadOnlyCollection<Parcel> Parcels => _parcels.AsReadOnly();
    
    public DeliveryPoint(int id, string address, string telegramAccount)
    {
        if (id <= 0) throw new ArgumentException("Id має бути > 0");
        Id = id;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        TelegramAccount = telegramAccount ?? throw new ArgumentNullException(nameof(telegramAccount));
        IsActive = true;
    }
    
    public void AddParcel(Parcel parcel)
    {
        if(parcel==null) throw new ArgumentNullException(nameof(parcel));
        if(parcel.DeliveryPointId!=Id)
        {
            parcel.ChangeStatus("Прибуло не на те відділення.");
            _parcels.Add(parcel);
        }
        else
        {
            parcel.ChangeStatus("ok");
            _parcels.Add(parcel);
        }
    }
    
}

public class Parcel
{
    public int Id{get; private set;}
    public int DeliveryPointId{get; private set;}
    public string Status{get; private set;}
    public Parcel(int id, int delivery_point_id)
    {
        Id = id;
        DeliveryPointId = delivery_point_id;
        Status = "process..";
    }
    
    public void ChangeStatus(string status)
    {
        Status = status;
    }
}