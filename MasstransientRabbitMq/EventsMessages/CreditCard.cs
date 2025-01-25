namespace Masstransient.RabbitMq.EventsMessages;

public class CreditCard
{
    public string? Holder { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpirationDate { get; set; }
    public string? SecurityCode { get; set; }

}
