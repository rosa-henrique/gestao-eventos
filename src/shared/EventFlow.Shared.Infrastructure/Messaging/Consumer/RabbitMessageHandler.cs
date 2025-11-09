using EventFlow.Shared.Infrastructure.Messaging.Contracts;

using Microsoft.Extensions.Logging;

namespace EventFlow.Shared.Infrastructure.Messaging.Consumer;

public abstract class RabbitMessageHandler<TMessage>
    where TMessage : IContract
{
    public abstract string QueueName { get; }
    public Task HandleAsync(TMessage message, CancellationToken cancellationToken)
    {
        return HandleMessageAsync(message, cancellationToken);
    }

    protected abstract Task HandleMessageAsync(TMessage message, CancellationToken cancellationToken);
}