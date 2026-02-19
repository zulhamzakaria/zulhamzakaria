using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.Extensions;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.Tenancy;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenants
{
    [Route("api/system/tenants")]
    [ApiController]
    public class GetTenantEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetTenantEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [IgnoreTenantResolution]
        [HttpGet]
        public async Task<IActionResult> GetTenants([FromQuery] TenantStatus? tenantStatus)
        {
            var query = new GetTenantsQuery(tenantStatus);
            var result = await _mediator
                .Send<GetTenantsQuery, Result<IReadOnlyCollection<GetTenantsDTO>>>(query);

            return result.ToActionResult();
        }
    }
}
