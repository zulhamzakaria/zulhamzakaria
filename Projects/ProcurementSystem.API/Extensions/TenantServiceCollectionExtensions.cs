using ProcurementSystem.API.SharedKernel.Infrastructure;

namespace ProcurementSystem.API.Extensions;

public static class TenantServiceCollectionExtensions
{
    public static IServiceCollection AddTenantSupport(this IServiceCollection services)
    {
        //services.AddScoped<ICurrentTenant, CurrentTenant>();
        services.AddScoped<CurrentTenant>();
        services.AddScoped<ICurrentTenant>(provider => provider.GetRequiredService<CurrentTenant>());
        return services;
    }
}
