using EventFlow.Shared.Infrastructure.HostedServices;
using EventFlow.Shared.Infrastructure.Messaging.Consumer;
using EventFlow.Shared.Infrastructure.Messaging.Contracts;
using EventFlow.Shared.Infrastructure.Messaging.RabbitTopology;
using EventFlow.Shared.Infrastructure.Messaging.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventFlow.Shared.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddMessaging(this IHostApplicationBuilder builder, string clientProvidedName)
    {
        builder.Services.Configure<RabbitTopologyOptions>(
            builder.Configuration.GetSection(RabbitTopologyOptions.SectionName));

        builder.AddRabbitMQClient(connectionName: "messaging");

        builder.Services.AddHostedService<RabbitTopologyInitializerHostedService>();
        builder.Services.AddSingleton<IMessagePublisher, RabbitMessagePublisher>();

        return builder;
    }

    public static IServiceCollection AddRabbitConsumer<THandler, TMessage>(this IServiceCollection services)
        where THandler : RabbitMessageHandler<TMessage>
        where TMessage : IContract
    {
        services.AddSingleton<THandler>();
        services.AddHostedService<RabbitMqConsumerService<TMessage, THandler>>();
        return services;
    }
}

