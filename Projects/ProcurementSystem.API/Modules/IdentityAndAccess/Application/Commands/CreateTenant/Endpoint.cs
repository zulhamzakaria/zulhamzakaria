using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant
{
    [Route("api/tenants")]
    [ApiController]
    public class Endpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public Endpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant
            ([FromBody] CreateTenantRequest request, CancellationToken ct)
        {
            var command = new CreateTenantCommand
                (request.TenantAlias,request.TenantName);

            var result = await _mediator.Send<CreateTenantCommand, Result<Guid>>
                (command, ct);

            if(result.IsFailure)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

    }
}
