using GestaoEventos.Domain.Compras.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace GestaoEventos.Application.Compras.Events;

public class CompraIngressoRealizadaEventHandler(ILogger<CompraIngressoRealizadaEventHandler> logger)
    : INotificationHandler<CompraIngressoRealizadaEvent>
{
    public Task Handle(CompraIngressoRealizadaEvent notification, CancellationToken cancellationToken)
    {
        logger.LogDebug("Informações do ingresso comprado {@Notification}", notification);
        return Task.CompletedTask;
    }
}