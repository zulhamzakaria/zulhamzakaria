using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;

public interface ITenantRepository
{
    Task<Tenant?> GetTenantByIdAsync
        (Guid tenantId, CancellationToken cancellationToken = default);
    Task<bool> IsTenantExistsAsync
        (Guid tenantId, CancellationToken cancellationToken = default);
    Task AddTenantAsync
        (Tenant tenant, CancellationToken cancellationToken = default);
}
