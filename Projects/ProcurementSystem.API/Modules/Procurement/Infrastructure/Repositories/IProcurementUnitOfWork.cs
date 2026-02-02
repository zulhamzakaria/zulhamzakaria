namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public interface IProcurementUnitOfWork
{
    IPurchaseRequestRepository RequestRepository { get; }
    IPurchaseOrderRepository OrderRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
