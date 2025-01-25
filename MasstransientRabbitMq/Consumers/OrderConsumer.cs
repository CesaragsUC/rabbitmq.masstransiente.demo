using Masstransient.RabbitMq.EventsMessages;
using MassTransit;
using Serilog;
using System.Text.Json;

namespace Masstransient.RabbitMq.Consumers;

public class OrderConsumer : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var result = JsonSerializer.Serialize<OrderCreatedEvent>(context.Message);
        Log.Information("Received Text: {Result}", result);

        return Task.CompletedTask;
    }
}

