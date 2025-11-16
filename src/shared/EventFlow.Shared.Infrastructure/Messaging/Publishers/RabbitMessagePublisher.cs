using System.Text.Json;

using EventFlow.Shared.Application.Contracts;
using EventFlow.Shared.Application.Interfaces;

using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace EventFlow.Shared.Infrastructure.Messaging.Publishers;

public class RabbitMessagePublisher(IConnection connection,
    ILogger<RabbitMessagePublisher> logger) : IMessagePublisher
{
    public async Task Publicar<T>(
        T mensagem,
        string exchange,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : IContract
    {
        if (mensagem is null)
        {
            throw new ArgumentNullException(nameof(mensagem));
        }

        if (string.IsNullOrWhiteSpace(exchange))
        {
            throw new ArgumentException("Exchange inválida.", nameof(exchange));
        }

        if (string.IsNullOrWhiteSpace(routingKey))
        {
            throw new ArgumentException("RoutingKey inválida.", nameof(routingKey));
        }

        var channel = await connection.CreateChannelAsync();

        var body = JsonSerializer.SerializeToUtf8Bytes(mensagem);

        var props = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json",
        };

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: props,
            body: body,
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "📤 Mensagem publicada na exchange '{Exchange}' com routingKey '{RoutingKey}'.",
            exchange,
            routingKey);
    }
}