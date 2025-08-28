using manta.Domain.Entities;
using manta.Domain.Enums;

namespace manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
        // var dp = new DeliveryPoint(10149, "м. Тарасівка, вул. Шевченка 2/6", "@10149tg");
        // var parcel1 = new Parcel(1,10149);
        // var parcel2 = new Parcel(2,10148);
        // Console.WriteLine($"parcel_id_{parcel1.Id} is {parcel1.Status}");
        // Console.WriteLine($"parcel_id_{parcel2.Id} is {parcel2.Status}");
        // dp.AddParcel(parcel1);
        // dp.AddParcel(parcel2);
        // Console.WriteLine($"parcel_id_{parcel1.Id} is {parcel1.Status}");
        // Console.WriteLine($"parcel_id_{parcel2.Id} is {parcel2.Status}");
        Parcel p = new Parcel(10149);
        Admin kk = new Admin(1, "Karpeta_Kyrylo", "notxceso@gmail.com");
        p.ChangeStatus(EParcelStatus.InTransit, kk);
        p.GetInfo();
        kk.PrintInfo();
    }
}