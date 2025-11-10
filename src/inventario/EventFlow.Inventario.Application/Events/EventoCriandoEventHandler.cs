using EventFlow.Inventario.Domain;
using EventFlow.Inventario.Domain.Events;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Inventario.Application.Events;

public class EventoCriandoEventHandler(IServiceScopeFactory serviceScopeFactory) : INotificationHandler<EventoCriadoEvent>
{
    public async Task Handle(EventoCriadoEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var eventoRepository = scope.ServiceProvider.GetRequiredService<IEventoRepository>();

        var evento = new Evento(notification.Id, notification.Status, notification.CriadoPor);

        eventoRepository.Adicionar(evento);

        await eventoRepository.SalvarAlteracoes(cancellationToken);
    }
}