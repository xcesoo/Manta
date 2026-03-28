using Manta.Contracts;

namespace Manta.Application.Interfaces;

public interface IIntegrationMessageQueue
{
    ValueTask EnqueueAsync<T>(T message, Guid messageId, CancellationToken ct) where T : class ;
    
    ValueTask<(object Message, Guid MessageId)> ReadAsync(CancellationToken ct);
    
    bool TryRead(out (object Message, Guid MessageId) item);
}