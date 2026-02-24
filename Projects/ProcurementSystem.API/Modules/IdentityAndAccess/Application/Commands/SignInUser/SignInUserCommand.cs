using ProcurementSystem.API.SharedKernel.Application.Messaging;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

public sealed record SignInUserCommand
    (string tenantAlias, string username, string password)
    : IRequest<object>;
