using EventFlow.Shared.Application;

using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Eventos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddDefaultApplication(typeof(DependencyInjection).Assembly);

        return services;
    }
}