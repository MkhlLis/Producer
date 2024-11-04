using System.Security.Cryptography;
using avro;
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

            await _producer.SendAsync(new User
            {
                id = Faker.RandomNumber.Next(10000, 99999),
                name = Faker.Name.FullName(),
                email = Faker.Internet.Email(),
            });

            await Task.Delay(10000, stoppingToken);
        }
    }
}