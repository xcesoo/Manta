using Manta.Domain.Enums;

namespace Manta.Domain.Entities;

public class ParcelStatus
{
    public EParcelStatus Status { get; private set; }
    public DateTime ChangedAt {get; private set;}
    public User? ChangedBy {get; private set;}

    public ParcelStatus(EParcelStatus status, User changedBy)
    {
        Status = status;
        ChangedBy = changedBy;
        ChangedAt = DateTime.UtcNow;
    }

}