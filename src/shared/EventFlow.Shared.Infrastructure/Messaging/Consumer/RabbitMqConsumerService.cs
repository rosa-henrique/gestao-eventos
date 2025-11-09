using System.Text;
using System.Text.Json;

using EventFlow.Shared.Infrastructure.Messaging.Contracts;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventFlow.Shared.Infrastructure.Messaging.Consumer;

public class RabbitMqConsumerService<TMessage, THandler>(
    THandler handler,
    IConnection connection,
    ILogger<RabbitMqConsumerService<TMessage, THandler>> logger) : BackgroundService
    where TMessage : IContract
    where THandler : RabbitMessageHandler<TMessage>
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queue = handler.QueueName;

        using var channel = connection.CreateModel();
        channel.BasicQos(0, 1, false);

        logger.LogInformation("Iniciando consumo da fila {Queue}", queue);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (_, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<TMessage>(json);
                if (message is null)
                {
                    logger.LogWarning("Mensagem nula recebida em {Queue}", queue);
                    channel.BasicAck(ea.DeliveryTag, false);
                    return;
                }

                await handler.HandleAsync(message, stoppingToken);
                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao consumir mensagem da fila {Queue}", queue);
                channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        channel.BasicConsume(queue, autoAck: false, consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}