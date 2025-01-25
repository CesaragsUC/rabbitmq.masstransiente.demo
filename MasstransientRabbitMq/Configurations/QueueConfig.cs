using Microsoft.Extensions.Configuration;

namespace Masstransient.RabbitMq.Configurations;

public static class QueueConfig
{
    public static string PaymentMessage => $"{EnvironmentPrefix()}.casoft.payments.v1";
    public static string OrderMessage => $"{EnvironmentPrefix()}.casoft.orders.v1";
    public static string DemoMessage => $"{EnvironmentPrefix()}.casoft.message.v1";

    private static IConfigurationBuilder GetConfigBuilder()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();

        return builder;
    }

    public static string EnvironmentPrefix()
    {
        var configuration = GetConfigBuilder().Build();

        return configuration?.GetSection("RabbitMqTransportOptions:Prefix").Value!;
    }

}

