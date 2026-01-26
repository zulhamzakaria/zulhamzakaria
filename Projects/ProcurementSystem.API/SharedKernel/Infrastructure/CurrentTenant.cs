
namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public sealed class CurrentTenant : ICurrentTenant
{
    public Guid TenantId { get; private set; }

    public bool IsResolved { get; private set; }

    public void SetTenant(Guid tenantId)
    {
        TenantId = tenantId;
        IsResolved = true;
    }

}
