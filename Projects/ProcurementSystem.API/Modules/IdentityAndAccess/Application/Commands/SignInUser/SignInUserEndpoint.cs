using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProcurementSystem.API.Extensions;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.Tenancy;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

[Route("api/users")]
[ApiController]
public class SignInUserEndpoint : ControllerBase
{
    private readonly IMediator _mediator;
    public SignInUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-in")]
    [EnableRateLimiting("SignInUser")]
    [IgnoreTenantResolution]
    public async Task<IActionResult> SignIn
        ([FromBody] SignInUserRequest request, CancellationToken ct)
    {
        var command = new SignInUserCommand(request.TenantAlias, request.Username, request.Password);
        var result = await _mediator.Send<SignInUserCommand, Result<string>>(command, ct);

        if(result.IsFailure)
            return result.ToActionResult();

        return Ok(new SignInUserResponse(result.Value));
    }
}

public sealed record SignInUserResponse(string Token);