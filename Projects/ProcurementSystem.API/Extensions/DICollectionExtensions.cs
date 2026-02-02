using ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Infrastructure;

namespace ProcurementSystem.API.Extensions;

public static class DICollectionExtensions
{
    public static IServiceCollection AddDIContainers(this IServiceCollection services)
    {
        services.AddScoped<IProcurementUnitOfWork, ProcurementUnitOfWork>();
        services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        return services;
    }
}
