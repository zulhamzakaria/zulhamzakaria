using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public class EfUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public EfUnitOfWork(TDbContext dbContext)
        => _dbContext = dbContext;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
