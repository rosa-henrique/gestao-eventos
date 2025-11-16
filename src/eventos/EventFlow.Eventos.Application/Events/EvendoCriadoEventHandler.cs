using EventFlow.Eventos.Domain.Events;
using EventFlow.Shared.Application.Contracts;
using EventFlow.Shared.Application.Interfaces;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace EventFlow.Eventos.Application.Events;

public class EvendoCriadoEventHandler(IMessagePublisher messagePublisher, IConfiguration configuration) : INotificationHandler<EventoCriadoEvent>
{
    public async Task Handle(EventoCriadoEvent notification, CancellationToken cancellationToken)
    {
        var exchange = configuration["Publisher:EventoCriado:Exchange"]!;
        var routingKey = configuration["Publisher:EventoCriado:RoutingKey"]!;

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