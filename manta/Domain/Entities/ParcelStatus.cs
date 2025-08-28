using manta.Domain.Enums;

namespace manta.Domain.Entities;

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

    public ParcelStatus(EParcelStatus status)
    {
        Status = status;
        ChangedBy = new Admin(1, "root", "root");
    }
    public override string ToString() => Status.ToString();
}