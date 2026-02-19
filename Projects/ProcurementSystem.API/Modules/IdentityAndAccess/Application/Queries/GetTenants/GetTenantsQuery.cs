using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenants;

public sealed record GetTenantsQuery(TenantStatus? TenantStatus) : IRequest<Result<IReadOnlyCollection<GetTenantsDTO>>>;

