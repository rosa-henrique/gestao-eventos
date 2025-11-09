using EventFlow.Inventario.Domain.Events;
using EventFlow.Inventario.Infrastructure.Persistence;
using EventFlow.Shared.Infrastructure;
using EventFlow.Shared.Infrastructure.HostedServices;
using EventFlow.Shared.Infrastructure.Messaging.Consumer;
using EventFlow.Shared.Infrastructure.Messaging.Contracts;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.Inventario.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        // builder.Services.AddHttpContextAccessor();
        builder.AddPersistence()
            .AddMessaging("EventoService");

        builder.Services.AddHostedService<DatabaseMigrationHostedService<InventarioDbContext>>();
        builder.Services.AddHostedService<RabbitTopologyInitializerHostedService>();

        builder.Services.AddRabbitConsumer<EventoCriadoHandler, EventoContract>();

        return builder;
    }

    private static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<InventarioDbContext>(connectionName: "postgresdb-inventario");

        builder.EnrichNpgsqlDbContext<InventarioDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.CommandTimeout = 30;
            });

        return builder;
    }
}

public sealed class EventoCriadoHandler(IPublisher publisher) : RabbitMessageHandler<EventoContract>
{
    public override string QueueName => "inventario.eventos.criado";
    protected override async Task HandleMessageAsync(EventoContract message, CancellationToken cancellationToken)
    {
        var status = Domain.StatusEvento.FromName(message.Status.ToString());
        var @event = new EventoCriadoEvent(message.Id, status, message.CriadoPor);
        await publisher.Publish(@event, cancellationToken);
    }
}