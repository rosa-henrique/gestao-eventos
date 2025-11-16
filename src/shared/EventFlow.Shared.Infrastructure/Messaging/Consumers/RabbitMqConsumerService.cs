using System.Text;
using System.Text.Json;

using EventFlow.Shared.Application.Contracts;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventFlow.Shared.Infrastructure.Messaging.Consumers;

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

        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        await channel.BasicQosAsync(0, 1, false, stoppingToken);

        logger.LogInformation("Iniciando consumo da fila {Queue}", queue);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<TMessage>(json);
                if (message is null)
                {
                    logger.LogWarning("Mensagem nula recebida em {Queue}", queue);
                    await channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
                    return;
                }

                await handler.HandleAsync(message, stoppingToken);
                await channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao consumir mensagem da fila {Queue}", queue);
                await channel.BasicNackAsync(ea.DeliveryTag, false, true, stoppingToken);
            }
        };

        await channel.BasicConsumeAsync(queue, autoAck: false, consumer, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}