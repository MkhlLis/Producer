namespace Producer;

public class Startup
{
    public static IHost IntitializeApp(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        Configure(builder.Services);

        var host = builder.Build();
        return host;
    }
    
    private static IServiceCollection Configure(IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        /*services.AddScoped<IAdministrationCommandHandler, AdministrationCommandHandler>();*/
        return services;
    }
}