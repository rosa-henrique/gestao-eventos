using System.Reflection;

using EventFlow.Shared.Application.PipelineBehaviors;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Shared.Application;

public static class DependencyInjection
{
    public static void AddDefaultApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
            options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
    }
}