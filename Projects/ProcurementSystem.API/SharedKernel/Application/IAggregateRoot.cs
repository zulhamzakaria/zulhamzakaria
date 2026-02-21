using ProcurementSystem.API.SharedKernel.Domain;

namespace ProcurementSystem.API.SharedKernel.Application;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
