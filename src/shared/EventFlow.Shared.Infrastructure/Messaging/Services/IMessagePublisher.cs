using EventFlow.Shared.Infrastructure.Messaging.Contracts;

namespace EventFlow.Shared.Infrastructure.Messaging.Services;

public interface IMessagePublisher
{
    Task Publicar<T>(
        T mensagem,
        string exchange,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : IContract;
}