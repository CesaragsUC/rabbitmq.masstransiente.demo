using Masstransient.RabbitMq.Configurations;
using Masstransient.RabbitMq.RabbitMq;
using Masstransient.RabbitMq.Utils;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Masstransient.RabbitMq.Workers;

public class WorkerDemo : IHostedService
{
    private readonly CancellationTokenSource _cts = new();
    private Task? _executingTask;
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _executingTask = DoWorkAsync(_cts.Token);

    }

    // The ideal would be create a worker for each message type, but here is just an example
    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                Log.Information("Worker running at: {0}", DateTimeOffset.Now);

                string host = "localhost";
                var instance = RabbitMqSingleton.GetInstance(host);

                var prefix = QueueConfig.DemoMessage;

                var messageCreatedEndpoint = await instance.Bus.GetSendEndpoint(new Uri($"queue:{QueueConfig.DemoMessage}"));
                await messageCreatedEndpoint.Send(FakeData.GenerateMessage());

                var paymentCreatedEndpoint = await instance.Bus.GetSendEndpoint(new Uri($"queue:{QueueConfig.PaymentMessage}"));
                await paymentCreatedEndpoint.Send(FakeData.GeneratePaymentMessage());

                var orderCreatedEndpoint = await instance.Bus.GetSendEndpoint(new Uri($"queue:{QueueConfig.OrderMessage}"));
                await orderCreatedEndpoint.Send(FakeData.GenerateOrderMessage());

                Log.Information("Worker finished at", DateTimeOffset.Now);

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message!);
            }

        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();

        if (_executingTask != null)
        {
            await _executingTask;
        }
    }
}