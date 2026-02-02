using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public interface IPurchaseRequestRepository
{
    Task<PurchaseRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
