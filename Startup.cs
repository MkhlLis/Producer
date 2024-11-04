using Confluent.Kafka;
using Producer.Implementations;
using Producer.Interfaces;

namespace Producer;

public static class Startup
{
    public static IHost IntitializeApp(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        // build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory().Replace("/bin/Debug/net8.0", ""))
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false)
            .Build();
        
        Configure(builder.Services, configuration);

        var host = builder.Build();
        return host;
    }
    
    private static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<Worker>();
        
        services.ConfigureKafka(configuration);
        services.AddSingleton<IMessageProducer, MessageProducer>();
        
        /*services.AddScoped<IAdministrationCommandHandler, AdministrationCommandHandler>();*/
        return services;
    }

    private static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProducerConfig>(configuration.GetSection("Kafka"));
        services.AddKafkaClient();
    }
}