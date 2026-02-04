namespace ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
