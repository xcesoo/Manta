using manta.Domain.Entities;
using manta.Domain.Enums;
using manta.Domain.Services;

namespace manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
         ParcelStatusService statusService = new ParcelStatusService();
         var dp = new DeliveryPoint(102, "blalba", statusService);
         var p = Parcel.Create(1, 103);
         var p2 = Parcel.Create(1, 102);
         dp.AddParcel(p, SystemUser.Instance);
         dp.AddParcel(p2, SystemUser.Instance);
         dp.DeliveryParcel(p2, SystemUser.Instance);
    }
}