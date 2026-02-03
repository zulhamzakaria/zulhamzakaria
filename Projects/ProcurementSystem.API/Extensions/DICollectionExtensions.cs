using ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Infrastructure;
using ProcurementSystem.API.SharedKernel.Security;

namespace ProcurementSystem.API.Extensions;

public static class DICollectionExtensions
{
    public static IServiceCollection AddDIContainers(this IServiceCollection services)
    {
        services.AddScoped<IProcurementUnitOfWork, ProcurementUnitOfWork>();
        services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
