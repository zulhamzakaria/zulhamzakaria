
namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public sealed class CurrentTenant : ICurrentTenant
{
    public Guid TenantId { get; private set; }

    public bool IsResolved => TenantId != Guid.Empty;

    public void ClearTenant()
    {
        TenantId = Guid.Empty;
    }

    public void SetTenant(Guid tenantId)
    {
        TenantId = tenantId;
    }

}
