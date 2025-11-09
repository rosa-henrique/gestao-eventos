using EventFlow.Inventario.Domain.Events;

using MediatR;

namespace EventFlow.Inventario.Application.Events;

public class EventoCriandoEventHandler : INotificationHandler<EventoCriadoEvent>
{
    public Task Handle(EventoCriadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}