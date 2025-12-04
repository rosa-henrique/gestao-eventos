using EventFlow.Inventario.Domain;
using EventFlow.Inventario.Infrastructure.IntegrationEvents;
using EventFlow.Inventario.Infrastructure.Persistence;
using EventFlow.Inventario.Infrastructure.Persistence.Repositories;
using EventFlow.Shared.Application.Contracts;
using EventFlow.Shared.Infrastructure;
using EventFlow.Shared.Infrastructure.HostedServices;
using EventFlow.Shared.UnitOfWork;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.Inventario.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddPersistence()
            .AddMessaging("EventoService");

        builder.Services.AddHostedService<DatabaseMigrationHostedService<InventarioDbContext>>();
        builder.Services.AddHostedService<RabbitTopologyInitializerHostedService>();

        builder.Services.AddRabbitConsumer<EventoCriadoIntegrationEvent, EventoContract>();
        builder.Services.AddRabbitConsumer<EventoAlteradoIntegrationEvent, EventoContract>();

        return builder;
    }

    private static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<InventarioDbContext>(connectionName: "sqlserver-inventario");

        builder.EnrichSqlServerDbContext<InventarioDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.CommandTimeout = 30;
            });

        builder.Services.AddScoped<IEventoRepository, EventoRepository>();
        builder.Services.AddScoped<IIngressoRepository, IngressoRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork<InventarioDbContext>>();

        return builder;
    }
}