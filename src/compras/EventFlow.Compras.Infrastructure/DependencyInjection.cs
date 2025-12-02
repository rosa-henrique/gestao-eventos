using EventFlow.Compras.Infrastructure.Persistence;
using EventFlow.Shared.Infrastructure;
using EventFlow.Shared.Infrastructure.HostedServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.Compras.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddPersistence()
            .AddMessaging("EventoService");

        builder.Services.AddHostedService<DatabaseMigrationHostedService<ComprasDbContext>>();
        builder.Services.AddHostedService<RabbitTopologyInitializerHostedService>();

        return builder;
    }

    private static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ComprasDbContext>(connectionName: "sqlserver-compras");

        builder.EnrichSqlServerDbContext<ComprasDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.CommandTimeout = 30;
            });

        return builder;
    }
}