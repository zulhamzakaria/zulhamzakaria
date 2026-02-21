using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Events;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.SharedKernel.Application;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.EventHandlers;

public class CreateSuperAdminOnTenantCreatedHandler : IDomainEventHandler<TenantCreatedDomainEvent>
{
    private readonly IADbContext _dbContext;
    public CreateSuperAdminOnTenantCreatedHandler(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(TenantCreatedDomainEvent domainEvent, CancellationToken ct)
    {
        var superAdmin = User.CreateSuperAdmin(domainEvent.TenantId);
        _dbContext.Users.Add(superAdmin.Value);
        await _dbContext.SaveChangesAsync(ct);
    }
}
