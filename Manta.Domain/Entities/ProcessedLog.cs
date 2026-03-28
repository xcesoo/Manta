namespace Manta.Domain.Entities;

public class ProcessedLog
{
    public Guid MessageId { get; set; }
    public DateTime ProcessedAt { get; set; }
}