
namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public class ProcurementUnitOfWork : IProcurementUnitOfWork
{
    public IPurchaseRequestRepository RequestRepository { get; }
    public IPurchaseOrderRepository OrderRepository { get; }
    private readonly ProcurementDbContext _dbContext;
    public ProcurementUnitOfWork(ProcurementDbContext dbContext, 
        IPurchaseRequestRepository requestRepository, IPurchaseOrderRepository orderRepository)
    {
        _dbContext = dbContext;
        RequestRepository = requestRepository;
        OrderRepository = orderRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
