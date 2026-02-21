using ProcurementSystem.API.SharedKernel.Domain;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Events;

public sealed record TenantCreatedDomainEvent
    (Guid TenantId, string Alias): IDomainEvent;
