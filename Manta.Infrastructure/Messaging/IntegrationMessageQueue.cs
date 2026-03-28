using System.Threading.Channels;
using Manta.Application.Interfaces;
using Manta.Contracts;

namespace Manta.Infrastructure.Messaging;

public class IntegrationMessageQueue : IIntegrationMessageQueue
{
    private readonly Channel<(object Message, Guid MessageId)> _channel;

    public IntegrationMessageQueue()
    {
        var options = new BoundedChannelOptions(100_000)
        {
            SingleWriter = false,
            SingleReader = true, 
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<(object, Guid)>(options);
    }

    public async ValueTask EnqueueAsync<T>(T message, Guid messageId, CancellationToken ct) where T : class
    {
        await _channel.Writer.WriteAsync((message, messageId), ct);
    }

    public async ValueTask<(object Message, Guid MessageId)> ReadAsync(CancellationToken ct)
    {
        return await _channel.Reader.ReadAsync(ct);
    }
    
    public bool TryRead(out (object Message, Guid MessageId) item)
    {
        return _channel.Reader.TryRead(out item);
    }
}