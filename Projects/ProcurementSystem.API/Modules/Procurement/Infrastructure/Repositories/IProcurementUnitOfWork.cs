namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public interface IProcurementUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
