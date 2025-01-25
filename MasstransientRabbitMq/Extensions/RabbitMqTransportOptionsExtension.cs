using MassTransit;

namespace Masstransient.RabbitMq.Extensions;

public static class RabbitMqTransportOptionsExtension
{
    public static string Prefix(this RabbitMqTransportOptions transportOptions)
    {
        return string.Empty;
    }
}
