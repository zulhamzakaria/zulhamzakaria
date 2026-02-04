
namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public class ProcurementUnitOfWork : IProcurementUnitOfWork
{
    private readonly ProcurementDbContext _dbContext;
    public ProcurementUnitOfWork(ProcurementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
