namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Repositories;

public interface IProcurementUnitOfWork
{
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    ISupplierRepository Suppliers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
