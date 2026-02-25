using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.SharedKernel.Application.Messaging;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

[Route("api/users")]
[ApiController]
public class SignInUserEndpoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SignInUserEndpoint> _logger;
    public SignInUserEndpoint
        (IMediator mediator, ILogger<SignInUserEndpoint> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
}
