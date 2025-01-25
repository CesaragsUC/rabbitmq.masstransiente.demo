namespace Masstransient.RabbitMq.EventsMessages;

public class PaymentCreatedEvent
{

    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int Method { get; set; }
    public int Status { get; set; }
    public CreditCard? CreditCard { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? TransactionId { get; set; }
}
