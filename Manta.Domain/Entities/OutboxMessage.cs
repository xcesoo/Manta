namespace Manta.Domain.Entities;

public class OutboxMessage
{
    public Guid MessageId { get; set; } 
    public string MessageType { get; set; } = string.Empty; 
    public string Payload { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
}