namespace EventFlow.Shared.Infrastructure.Messaging.RabbitTopology;

public class RabbitTopologyOptions
{
    public const string SectionName = "RabbitTopology";
    public IList<ExchangeConfig>? Exchanges { get; set; }
    public IList<QueueConfig>? Queues { get; set; }

    public bool HasAnyConfig => (Exchanges?.Any() == true) || (Queues?.Any() == true);
}

public class ExchangeConfig
{
    public string? Name { get; set; }
    public string? Type { get; set; } = "direct";
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}

public class QueueConfig
{
    public string? Name { get; set; }
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public IList<BindingConfig>? Bindings { get; set; }
    public IDictionary<string, object>? Arguments { get; set; }
}

public class BindingConfig
{
    public string? Exchange { get; set; }
    public string? RoutingKey { get; set; }
}