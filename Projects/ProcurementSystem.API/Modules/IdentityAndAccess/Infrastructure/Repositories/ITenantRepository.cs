using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;

public interface ITenantRepository
{
    Task<Tenant?> GetTenantByIdAsync
        (Guid tenantId, CancellationToken cancellationToken = default);
    Task<Guid?> GetTenantIdByAliasAsync
            (string tenantAlias, CancellationToken cancellationToken = default);
    Task<bool> IsTenantExistsByAliasAsync
        (string tenantAlias, CancellationToken cancellationToken = default);
    Task<bool> IsTenantExistsByNameAsync
        (string tenantName, CancellationToken cancellationToken = default);
    void AddTenant
        (Tenant tenant);
}
