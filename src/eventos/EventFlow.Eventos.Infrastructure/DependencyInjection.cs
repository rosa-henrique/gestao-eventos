using EventFlow.Eventos.Domain;
using EventFlow.Eventos.Infrastructure.Persistence;
using EventFlow.Eventos.Infrastructure.Persistence.Repositories;
using EventFlow.Shared.Infrastructure;
using EventFlow.Shared.Infrastructure.HostedServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.Eventos.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        // builder.Services.AddHttpContextAccessor();
        builder.AddPersistence()
            .AddMessaging("EventoService");

        builder.Services.AddHostedService<DatabaseMigrationHostedService<EventosDbContext>>();
        builder.Services.AddHostedService<RabbitTopologyInitializerHostedService>();

        return builder;
    }

    private static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<EventosDbContext>(connectionName: "postgresdb-eventos");

        builder.EnrichNpgsqlDbContext<EventosDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.CommandTimeout = 30;
            });

        builder.Services.AddScoped<IEventoRepository, EventoRepository>();

        return builder;
    }
}