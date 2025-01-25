using Masstransient.RabbitMq.EventsMessages;
using MassTransit;
using Serilog;
using System.Text.Json;

namespace Masstransient.RabbitMq.Consumers;

public class PaymentConsumer : IConsumer<PaymentCreatedEvent>
{
    public Task Consume(ConsumeContext<PaymentCreatedEvent> context)
    {
        var result = JsonSerializer.Serialize<PaymentCreatedEvent>(context.Message);
        Log.Information("Received Text: {Result}", result);
        return Task.CompletedTask;
    }
}

