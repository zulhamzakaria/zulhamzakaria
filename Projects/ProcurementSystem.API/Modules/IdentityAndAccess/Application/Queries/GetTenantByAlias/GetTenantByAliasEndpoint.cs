using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.Extensions;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.Tenancy;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantByAlias;

[Route("api/system/tenants")]
[ApiController]
public class GetTenantByAliasEndpoint : ControllerBase
{
    private readonly IMediator _mediator;
    public GetTenantByAliasEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [IgnoreTenantResolution]
    [HttpGet]
    public async Task<IActionResult> GetTenantByAlias(string alias)
    {
        var query = new GetTenantByAliasQuery(alias);
        var result = await _mediator
            .Send<GetTenantByAliasQuery, Result<GetTenantByAliasDTO>>(query);
        return result.ToActionResult();
    }
}
