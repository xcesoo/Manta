using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Exceptions;

public class ParcelDomainException : Exception
{
    public int ParcelId { get; }
    public int? DeliveryPointId { get; }
    public string CurrentStatus { get; }
    public ERuleResultError ErrorCode { get; }

    public ParcelDomainException(Parcel parcel, DeliveryPoint deliveryPoint, RuleResult result) : base(result.Message)
    {
        ParcelId = parcel.Id;
        DeliveryPointId = deliveryPoint?.Id;
        CurrentStatus = parcel.CurrentStatus.Status.ToString();
        ErrorCode = result.Code ?? ERuleResultError.Unknown;
    }
}