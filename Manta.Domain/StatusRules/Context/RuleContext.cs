using Manta.Domain.Entities;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Context;

public sealed record RuleContext
{
    public Parcel Parcel { get; init; }
    public UserInfo User { get; init; }
    public DeliveryPoint? DeliveryPoint { get; init; }
    public DeliveryVehicle? DeliveryVehicle { get; init; }
    public int ActiveParcelsCount { get; init; }

    public static RuleContext ForParcel(Parcel parcel, UserInfo user) =>
        new RuleContext
        {
            Parcel = parcel,
            User = user
        };

    public static RuleContext ForDelivery(Parcel parcel, UserInfo user, DeliveryPoint deliveryPoint, int activeParcelsCount = 0) =>
        new RuleContext
        {
            Parcel = parcel,
            User = user,
            DeliveryPoint = deliveryPoint,
            ActiveParcelsCount = activeParcelsCount
        };

    public static RuleContext ForVehicle(Parcel parcel, UserInfo user, DeliveryVehicle deliveryVehicle) =>
        new RuleContext()
        {
            Parcel = parcel,
            User = user,
            DeliveryVehicle = deliveryVehicle
        };
}