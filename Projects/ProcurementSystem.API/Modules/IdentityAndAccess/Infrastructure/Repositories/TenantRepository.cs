using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.SharedKernel.Enums;

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

    public async Task<Tenant?> GetTenantByIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
        => await _dbContext.Tenants
        .SingleOrDefaultAsync(t => t.Id == tenantId, cancellationToken);

    public async Task<Guid?> GetTenantIdByAliasAsync(string tenantAlias, CancellationToken cancellationToken = default)
        => await _dbContext.Tenants.AsNoTracking()
        .Where(t => t.TenantAlias == tenantAlias && t.TenantStatus == TenantStatus.Active)
        .Select(t => t.Id)
        .SingleOrDefaultAsync(cancellationToken);


    public async Task<bool> IsTenantExistsByAliasAsync(string tenantAlias, CancellationToken cancellationToken = default)
        => await _dbContext.Tenants
        .AnyAsync(t => t.TenantAlias == tenantAlias, cancellationToken);

    public async Task<bool> IsTenantExistsByNameAsync(string tenantName, CancellationToken cancellationToken = default)
        => await _dbContext.Tenants
        .AnyAsync(t => t.TenantName == tenantName, cancellationToken);
}
