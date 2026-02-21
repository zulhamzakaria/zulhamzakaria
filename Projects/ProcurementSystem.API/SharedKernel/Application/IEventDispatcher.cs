using ProcurementSystem.API.SharedKernel.Domain;

namespace ProcurementSystem.API.SharedKernel.Application;

public interface IEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct);
}
