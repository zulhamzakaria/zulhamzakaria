using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantById;

public sealed record GetTenantByIdQuery
    (Guid Id) : IRequest<Result<GetTenantByIdDTO>>;
