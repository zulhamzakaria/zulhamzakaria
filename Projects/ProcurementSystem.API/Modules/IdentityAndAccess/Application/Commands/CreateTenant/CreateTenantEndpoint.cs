using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.Extensions;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.Tenancy;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

[Route("api/system/tenants")]
[ApiController]
public class CreateTenantEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTenantEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [IgnoreTenantResolution]
    [HttpPost]
    public async Task<IActionResult> CreateTenant
        ([FromBody] CreateTenantRequest request, CancellationToken ct)
    {
        var command = new CreateTenantCommand
            (request.TenantAlias, request.TenantName);

        var result = await _mediator.Send<CreateTenantCommand, Result<Guid>>
            (command, ct);

        return result.ToActionResult();
    }

}
