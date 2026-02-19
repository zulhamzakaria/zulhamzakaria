using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantByAlias;

public sealed record GetTenantByAliasQuery
    (string Alias) : IRequest<Result<GetTenantByAliasDTO>>;
