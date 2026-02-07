using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenantsController : ControllerBase
{
    private readonly IMediator _mediator;
    public TenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTenant
        ([FromBody] CreateTenantRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTenantCommand
            (request.TenantName, request.TenantAlias);

        var result = await _mediator.Send<CreateTenantCommand, Result<Guid>>
            (command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(CreateTenant), new { id = result.Value }, null);
    }
}
