using GestaoEventos.Domain.Eventos;
using GestaoEventos.Infrastructure.Persistence;
using GestaoEventos.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

namespace GestaoEventos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = GestaoEventos.sqlite"));

        services.AddScoped<IEventoRepository, EventoRepository>();

        return services;
    }
}