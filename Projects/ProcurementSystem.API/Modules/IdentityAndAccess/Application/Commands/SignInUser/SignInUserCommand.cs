using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

public sealed record SignInUserCommand
    (string TenantAlias, string Username, string Password)
    : IRequest<Result<string>>;
