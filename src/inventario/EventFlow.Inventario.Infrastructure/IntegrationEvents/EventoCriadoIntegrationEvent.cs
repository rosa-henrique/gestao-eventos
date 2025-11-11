using EventFlow.Inventario.Domain.Events;
using EventFlow.Shared.Infrastructure.Messaging.Consumer;
using EventFlow.Shared.Infrastructure.Messaging.Contracts;

using MediatR;

namespace EventFlow.Inventario.Infrastructure.IntegrationEvents;

public class EventoCriadoIntegrationEvent(IPublisher publisher) : RabbitMessageHandler<EventoContract>
{
    public override string QueueName => "inventario.eventos.criado";
    protected override async Task HandleMessageAsync(EventoContract message, CancellationToken cancellationToken)
    {
        var status = Domain.StatusEvento.FromName(message.Status.ToString());
        var @event = new EventoCriadoEvent(message.Id, message.CapacidadeMaxima, status, message.CriadoPor);
        await publisher.Publish(@event, cancellationToken);
    }
}