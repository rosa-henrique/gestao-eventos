using EventFlow.Eventos.Domain.Events;
using EventFlow.Shared.Application.Contracts;
using EventFlow.Shared.Application.Interfaces;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace EventFlow.Eventos.Application.Events;

public class EvendoAlteradoEventHandler(IMessagePublisher messagePublisher, IConfiguration configuration) : INotificationHandler<EventoAlteradoEvent>
{
    public async Task Handle(EventoAlteradoEvent notification, CancellationToken cancellationToken)
    {
        var exchange = configuration["Publisher:EventoAlterado:Exchange"]!;
        var routingKey = configuration["Publisher:EventoAlterado:RoutingKey"]!;

        var evento = notification.Evento;
        var contract = new EventoContract(
            evento.Id,
            evento.Nome,
            evento.DataHoraInicio,
            evento.DataHoraFim,
            evento.Localizacao,
            evento.CapacidadeMaxima,
            Enum.Parse<StatusEvento>(evento.Status.Name),
            evento.CriadoPor);

        await messagePublisher.Publicar(
            contract,
            exchange: exchange,
            routingKey: routingKey,
            cancellationToken);
    }
}