using FluentValidation;

using GestaoEventos.Application.Common.Behaviors;

using Microsoft.Extensions.DependencyInjection;

namespace GestaoEventos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(AuthorizeCriadorEventoBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}