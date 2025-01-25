using MassTransit;

namespace Masstransient.RabbitMq.RabbitMq;

/// <summary>
/// Is a class to extend the RabbitMqTransportOptions
/// Here we can add more properties to the RabbitMqTransportOptions
/// </summary>
public class RabbitMqConfig : RabbitMqTransportOptions
{
    public string? Prefix { get; set; }
}
