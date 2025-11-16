using EventFlow.Shared.Application.Contracts;

namespace EventFlow.Shared.Application.Interfaces;

public interface IMessagePublisher
{
    Task Publicar<T>(
        T mensagem,
        string exchange,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : IContract;
}