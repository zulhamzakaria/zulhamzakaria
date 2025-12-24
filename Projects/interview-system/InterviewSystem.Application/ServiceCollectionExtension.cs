using Microsoft.Extensions.DependencyInjection;

namespace InterviewSystem.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, string licenseKey)
    {
        services.AddMediatR(
            cfg => { 
                cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly); 
                cfg.LicenseKey = licenseKey;
            });
        return services;
    }
}
