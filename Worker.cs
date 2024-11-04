using Producer.Interfaces;

namespace Producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageProducer _producer;

    public Worker(ILogger<Worker> logger, IMessageProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            _producer.SendAsync<string>("hello");

            await Task.Delay(10000, stoppingToken);
        }
    }
}