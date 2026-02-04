namespace ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

public interface ICurrentTenant
{
    Guid TenantId { get; }
    bool IsResolved { get; }

    void SetTenant(Guid tenantId);
    void ClearTenant();
}
