namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public interface ICurrentTenant
{
    Guid TenantId { get; }
    bool IsResolved { get; }

    void SetTenant(Guid tenantId);
    void ClearTenant();
}
