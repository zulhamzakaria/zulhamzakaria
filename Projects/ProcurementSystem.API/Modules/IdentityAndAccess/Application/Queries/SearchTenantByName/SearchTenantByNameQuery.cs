using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.SearchTenantByName;

public sealed record SearchTenantByNameQuery
    (string Name) : IRequest<Result<IReadOnlyCollection<SearchTenantByNameDTO>>>;
