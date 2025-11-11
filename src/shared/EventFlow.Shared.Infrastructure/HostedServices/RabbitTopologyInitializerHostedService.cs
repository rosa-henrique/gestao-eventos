using EventFlow.Shared.Infrastructure.Messaging;
using EventFlow.Shared.Infrastructure.Messaging.RabbitTopology;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

namespace EventFlow.Shared.Infrastructure.HostedServices;

public class RabbitTopologyInitializerHostedService(
    IConnection connection,
    IOptions<RabbitTopologyOptions> rabbitTopologyOptions,
    ILogger<RabbitTopologyInitializerHostedService> logger) : IHostedService
{
    private readonly RabbitTopologyOptions rabbitTopology = rabbitTopologyOptions?.Value ?? new RabbitTopologyOptions();
    private IModel? _channel;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        if (!rabbitTopology.HasAnyConfig)
        {
            logger.LogInformation("ℹ️ Nenhuma configuração de topologia RabbitMQ encontrada. Ignorando inicialização.");
            return Task.CompletedTask;
        }

        logger.LogInformation("🎯 Iniciando configuração de topologia RabbitMQ...");

        _channel = connection.CreateModel();

        try
        {
            if (rabbitTopology.Exchanges?.Any() == true)
            {
                CriarExchanges();
            }

            if (rabbitTopology.Queues?.Any() == true)
            {
                CriarQueuesEBindings();
            }

            logger.LogInformation("✅ Topologia RabbitMQ configurada com sucesso!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Falha ao configurar topologia do RabbitMQ.");
            throw;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Dispose();
        return Task.CompletedTask;
    }

    private void CriarExchanges()
    {
        foreach (var exchange in rabbitTopology.Exchanges!)
        {
            if (string.IsNullOrWhiteSpace(exchange.Name))
            {
                logger.LogWarning("⚠️ Exchange com nome inválido ignorada.");
                continue;
            }

            _channel!.ExchangeDeclare(
                exchange: exchange.Name,
                type: exchange.Type ?? ExchangeType.Direct,
                durable: exchange.Durable,
                autoDelete: exchange.AutoDelete);

            logger.LogInformation("📦 Exchange declarada: {Exchange} ({Type})", exchange.Name,
                exchange.Type ?? "direct");
        }
    }

    private void CriarQueuesEBindings()
    {
        foreach (var queue in rabbitTopology.Queues!)
        {
            if (string.IsNullOrWhiteSpace(queue.Name))
            {
                logger.LogWarning("⚠️ Fila com nome inválido ignorada.");
                continue;
            }

            _channel!.QueueDeclare(
                queue: queue.Name,
                durable: queue.Durable,
                exclusive: queue.Exclusive,
                autoDelete: queue.AutoDelete,
                arguments: queue.Arguments);

            logger.LogInformation("🧩 Fila declarada: {Queue}", queue.Name);

            if (queue.Bindings?.Any() != true)
            {
                logger.LogInformation("🧩 Não há bindings: {Queue}", queue.Name);
                continue;
            }

            foreach (var binding in queue.Bindings)
            {
                if (string.IsNullOrWhiteSpace(binding.Exchange) ||
                    string.IsNullOrWhiteSpace(binding.RoutingKey))
                {
                    logger.LogWarning("⚠️ Binding inválido ignorado (Exchange ou RoutingKey ausentes).");
                    continue;
                }

                _channel.QueueBind(
                    queue: queue.Name,
                    exchange: binding.Exchange,
                    routingKey: binding.RoutingKey);

                logger.LogInformation(
                    "🔗 Binding criado: {Queue} ← {Exchange} ({RoutingKey})",
                    queue.Name, binding.Exchange, binding.RoutingKey);
            }
        }
    }
}