using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.Extensions;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantById
{
    [Route("api/system/tenants")]
    [ApiController]
    public class GetTenantByIdEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetTenantByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTenantById(GetTenantByIdQuery query)
        { 
            var result = await _mediator
                .Send<GetTenantByIdQuery, Result<GetTenantByIdDTO>>(query);

            return result.ToActionResult();
        }
    }
}
