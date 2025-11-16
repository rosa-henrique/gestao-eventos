using EventFlow.Shared.Application.Contracts;

namespace EventFlow.Shared.Infrastructure.Messaging.Consumers;

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