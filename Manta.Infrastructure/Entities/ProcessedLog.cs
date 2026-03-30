namespace Manta.Infrastructure.Entities;

public class ProcessedLog
{
    public Guid MessageId { get; set; }
    public DateTime ProcessedAt { get; set; }
}