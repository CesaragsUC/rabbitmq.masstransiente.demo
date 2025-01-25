using MassTransit;

namespace Masstransient.RabbitMq.RabbitMq;

public class RabbitMqSingleton
{
    private RabbitMqSingleton() { }

    private static RabbitMqSingleton Instance;

    private static readonly object _lock = new object();
    public Guid InstanceId { get; set; }
    public IBusControl Bus { get; set; }


    public static RabbitMqSingleton GetInstance()
    {
        return Instance;
    }
    public static RabbitMqSingleton GetInstance(string host)
    {
        if (Instance == null)
        {
            lock (_lock)
            {
                if (Instance == null)
                {
                    Instance = new RabbitMqSingleton
                    {
                        InstanceId = Guid.NewGuid()
                    };
                    Instance.Bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(sbc =>
                    {
                        sbc.Host(host);
                    });
                    Instance.Bus.StartAsync().Wait();
                }
            }
        }
        return Instance;
    }
    public static RabbitMqSingleton GetInstance(Action<IRabbitMqBusFactoryConfigurator> configure)
    {
        if (Instance == null)
        {
            lock (_lock)
            {
                if (Instance == null)
                {
                    Instance = new RabbitMqSingleton
                    {
                        InstanceId = Guid.NewGuid()
                    };
                    Instance.Bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(configure);
                    Instance.Bus.StartAsync().Wait();
                }
            }
        }
        return Instance;
    }

    public static bool IsInstanceCreated()
    {
        return Instance != null;
    }
}