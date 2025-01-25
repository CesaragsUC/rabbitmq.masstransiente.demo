namespace Masstransient.RabbitMq.EventsMessages;

public class OrderCreatedEvent
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }
}