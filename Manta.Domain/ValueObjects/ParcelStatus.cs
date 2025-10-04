using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Domain.ValueObjects;

public sealed record  ParcelStatus
{
    public EParcelStatus Status { get; }
    public DateTime ChangedAt { get; }
    public UserInfo ChangedBy { get; }

    public ParcelStatus(EParcelStatus status, User changedBy)
    {
        Status = status;
        ChangedBy = changedBy;
        ChangedAt = DateTime.UtcNow;
    } 
    public override string ToString() => $"{Status} at {ChangedAt} by {ChangedBy}";
    public static implicit operator string(ParcelStatus parcelStatus) => parcelStatus.ToString();
}