using Bogus;
using Masstransient.RabbitMq.EventsMessages;

namespace Masstransient.RabbitMq.Utils;

public static class FakeData
{
    public static MessageCreateEvent GenerateMessage()
    {
        Faker faker = new();

        var message = new MessageCreateEvent
        {
            Text = faker.Person.FullName
        };
        return message;
    }

    public static PaymentCreatedEvent GeneratePaymentMessage()
    {
        Faker faker = new();

        var message = new PaymentCreatedEvent
        {
            Amount = faker.Finance.Amount(),
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Method = faker.Random.Int(1, 3),
            OrderId = Guid.NewGuid(),
            PaymentDate = faker.Date.Past(),
            Status = faker.Random.Int(1, 3),
            TransactionId = faker.Random.String2(10),
            CreditCard = new CreditCard
            {
                CardNumber = faker.Finance.CreditCardNumber(),
                ExpirationDate = "12/27",
                SecurityCode = faker.Finance.CreditCardCvv()
            }
        };
        return message;
    }

    public static OrderCreatedEvent GenerateOrderMessage()
    {
        Faker faker = new();

        var message = new OrderCreatedEvent
        {
            CreatedAt = faker.Date.Past(),
            CustomerId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            Status = faker.Random.Int(1, 3),
            TotalAmount = faker.Finance.Amount()
        };
        return message;
    }
}
