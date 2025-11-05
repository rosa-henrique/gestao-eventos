using EventFlow.Eventos.Domain;
using EventFlow.Eventos.Infrastructure.Persistence;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.Eventos.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        // builder.Services.AddHttpContextAccessor();
        builder.AddPersistence();

        return builder;
    }

    public static WebApplication UseMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<EventosDbContext>();
            db.Database.Migrate();
        }

        return app;
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