using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public class PurchaseRequestRepository : IPurchaseRequestRepository
{
    private readonly ProcurementDbContext _dbContext;
    public PurchaseRequestRepository(ProcurementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PurchaseRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.PurchaseRequests
            .Include(pr => pr.Items)
            .FirstOrDefaultAsync(pr => pr.Id == id, cancellationToken);
}
