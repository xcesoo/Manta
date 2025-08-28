    using manta.Domain.Entities;
    using manta.Domain.Enums;

    namespace manta.Domain.Services;

    public class ParcelStatusService
    {

        public void UpdateStatus(Parcel parcel, User changedBy, DeliveryPoint deliveryPoint)
        {
            if (parcel.DeliveryPointId != deliveryPoint.Id) parcel.ChangeStatus(EParcelStatus.WrongLocation, changedBy);
            else parcel.ChangeStatus(EParcelStatus.Delivered, changedBy);
        }
    }