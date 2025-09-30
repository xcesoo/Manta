using Manta.Domain.Entities;

namespace Manta.Domain.StatusRules;

public sealed record RuleContext
{
    public Parcel Parcel { get; init; }
    public User User { get; init; }
    public DeliveryPoint? DeliveryPoint { get; init; }
    public DeliveryVehicle? DeliveryVehicle { get; init; }

    public static RuleContext ForParcel(Parcel parcel, User user) =>
        new RuleContext
        {
            Parcel = parcel,
            User = user
        };

    public static RuleContext ForDelivery(Parcel parcel, User user, DeliveryPoint deliveryPoint) =>
        new RuleContext
        {
            Parcel = parcel,
            User = user,
            DeliveryPoint = deliveryPoint
        };

    public static RuleContext ForVehicle(Parcel parcel, User user, DeliveryVehicle deliveryVehicle) =>
        new RuleContext()
        {
            Parcel = parcel,
            User = user,
            DeliveryVehicle = deliveryVehicle
        };
}