using Masstransient.RabbitMq.Consumers;
using Masstransient.RabbitMq.EventsMessages;
using Masstransient.RabbitMq.Exceptions;
using Masstransient.RabbitMq.RabbitMq;
using Masstransient.RabbitMq.Workers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Authentication;

namespace Masstransient.RabbitMq.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitSetup(this IServiceCollection services)
    {

        var configuration = GetConfigBuilder().Build();

        var rabbitMqOptions = new RabbitMqConfig();
        configuration.GetSection("RabbitMqTransportOptions").Bind(rabbitMqOptions);

        services.AddMassTransit(x =>
        {

            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqOptions.Host, host =>
                {
                    host.Username(rabbitMqOptions.User);
                    host.Password(rabbitMqOptions.Pass);

                    if (rabbitMqOptions.UseSsl)
                        host.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12);
                });


                ConfigureEndpoint<MessageConsumer>(context, cfg,
                    GetRabbitEndpointConfig(nameof(MessageCreateEvent), QueueConfig.DemoMessage));

                ConfigureEndpoint<OrderConsumer>(context, cfg,
                    GetRabbitEndpointConfig(nameof(OrderCreatedEvent), QueueConfig.OrderMessage));

                ConfigureEndpoint<PaymentConsumer>(context, cfg,
                    GetRabbitEndpointConfig(nameof(PaymentCreatedEvent), QueueConfig.PaymentMessage));

            });

        });

        services.AddHostedService<WorkerDemo>();

        return services;
    }

    static void ConfigureEndpoint<TConsumer>(
        IBusRegistrationContext context,
        IRabbitMqBusFactoryConfigurator configRabbit,
        RabbitMqEndpointConfig endpointConfig)
        where TConsumer : class, IConsumer
    {
        configRabbit.ReceiveEndpoint(endpointConfig.QueueName!, configureEndpoint =>
        {
            configureEndpoint.ConfigureConsumeTopology = endpointConfig.ConfigureConsumeTopology;
            configureEndpoint.PrefetchCount = endpointConfig.PrefetchCount;
            configureEndpoint.UseMessageRetry(retry =>
            {
                retry.Interval(endpointConfig.RetryLimit, endpointConfig.Interval);
                retry.Ignore<ConsumerCanceledException>();
                retry.Exponential(3, TimeSpan.FromSeconds(5), TimeSpan.FromHours(1), TimeSpan.FromSeconds(10))
                .Handle<CasoftStoreRetryException>();
            });

            configureEndpoint.ConfigureConsumer<TConsumer>(context);

        });
    }

    private static RabbitMqEndpointConfig GetRabbitEndpointConfig(string routingKey, string queueName)
    {
        return new RabbitMqEndpointConfig
        {
            QueueName = queueName,
            RoutingKey = routingKey,
            ExchangeType = RabbitMQ.Client.ExchangeType.Fanout,
            RetryLimit = 2,
            Interval = TimeSpan.FromSeconds(3),
            ConfigureConsumeTopology = false,
            PrefetchCount = 5
        };
    }

    private static IConfigurationBuilder GetConfigBuilder()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();

        return builder;
    }
}
