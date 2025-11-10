using EventFlow.Inventario.Domain;
using EventFlow.Inventario.Domain.Events;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventFlow.Inventario.Application.Events;

public class EventoAlteradoEventHandler(ILogger<EventoAlteradoEventHandler> logger, IServiceScopeFactory serviceScopeFactory) : INotificationHandler<EventoAlteradoEvent>
{
    public async Task Handle(EventoAlteradoEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var eventoRepository = scope.ServiceProvider.GetRequiredService<IEventoRepository>();

        var evento = await eventoRepository.BuscarPorId(notification.Id, cancellationToken);

        if (evento is null)
        {
            logger.LogError("Evento não encontrado para alteração");
            return;
        }

        evento.Alterar(notification.Status);
        eventoRepository.Alterar(evento);

        await eventoRepository.SalvarAlteracoes(cancellationToken);
    }
}