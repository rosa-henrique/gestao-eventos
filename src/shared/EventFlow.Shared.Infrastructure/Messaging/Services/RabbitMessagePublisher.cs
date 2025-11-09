using System.Text.Json;

using EventFlow.Shared.Infrastructure.Messaging.Contracts;

using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace EventFlow.Shared.Infrastructure.Messaging.Services;

public class RabbitMessagePublisher(IConnection connection,
    ILogger<RabbitMessagePublisher> logger) : IMessagePublisher
{
    public Task Publicar<T>(
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

        var channel = connection.CreateModel();

        var body = JsonSerializer.SerializeToUtf8Bytes(mensagem);

        var props = channel.CreateBasicProperties();
        props.Persistent = true;
        props.ContentType = "application/json";

        channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: props,
            body: body);

        logger.LogInformation(
            "📤 Mensagem publicada na exchange '{Exchange}' com routingKey '{RoutingKey}'.",
            exchange,
            routingKey);

        return Task.CompletedTask;
    }
}