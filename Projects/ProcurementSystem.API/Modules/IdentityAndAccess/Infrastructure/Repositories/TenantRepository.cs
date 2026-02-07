using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly IADbContext _dbContext;
    public TenantRepository(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddTenant(Tenant tenant)
        => _dbContext.Tenants.Add(tenant);

    public Task<Tenant?> GetTenantByIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
        => _dbContext.Tenants
        .SingleOrDefaultAsync(t => t.Id == tenantId, cancellationToken);

    public Task<bool> IsTenantExistsAsync(string tenantAlias, CancellationToken cancellationToken = default)
        => _dbContext.Tenants
        .AnyAsync(t => t.TenantAlias == tenantAlias, cancellationToken);
}
