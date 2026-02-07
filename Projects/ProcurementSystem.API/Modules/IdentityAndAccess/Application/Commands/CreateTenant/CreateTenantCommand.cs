using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

public sealed record CreateTenantCommand 
    (string TenantAlias, string TenantName)
    : IRequest<Result<Guid>>;
