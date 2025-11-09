using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventFlow.Shared.Infrastructure.HostedServices;

public class DatabaseMigrationHostedService<TContext>(
    IServiceProvider serviceProvider,
    ILogger<DatabaseMigrationHostedService<TContext>> logger) : IHostedService
    where TContext : DbContext
{
    private const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            await RunMigrationAsync(dbContext, logger, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task RunMigrationAsync(TContext dbContext, ILogger<DatabaseMigrationHostedService<TContext>> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("⏳ Iniciando execução de migrations para {DbContext}...", typeof(TContext).Name);

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });

        logger.LogInformation("✅ Migrations aplicadas com sucesso para {DbContext}.", typeof(TContext).Name);
    }
}