namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public interface ITenantEntity
{
    public Guid TenantId { get; set; }
}
