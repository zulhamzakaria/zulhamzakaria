namespace ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

public interface ITenantEntity
{
    public Guid TenantId { get; set; }
}
