using FluentValidation;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;
using ProcurementSystem.API.Modules.Procurement.Infrastructure;
using ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Application.Behaviors;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.Infrastructure;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;
using ProcurementSystem.API.SharedKernel.Infrastructure.Messaging;
using ProcurementSystem.API.SharedKernel.Security;

namespace ProcurementSystem.API.Extensions;

public static class DICollectionExtensions
{
    public static IServiceCollection AddDIContainers(this IServiceCollection services)
    {
        services.AddScoped<IProcurementUnitOfWork, ProcurementUnitOfWork>();
        services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork<ProcurementDbContext>>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork<IADbContext>>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IMediator, Mediator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Scan(scan => scan
        .FromAssemblyOf<AssemblyMarker>()
        .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());


        return services;
    }
}
