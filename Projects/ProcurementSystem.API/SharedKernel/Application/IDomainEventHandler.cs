using ProcurementSystem.API.SharedKernel.Domain;

namespace ProcurementSystem.API.SharedKernel.Application;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken ct);
}
