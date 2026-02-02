using ProcurementSystem.API.SharedKernel.Infrastructure;
using ProcurementSystem.API.Tenancy;

namespace ProcurementSystem.API.Middleware;

public sealed class TenantResolutionMiddleware
{
    private readonly RequestDelegate _request;

    public TenantResolutionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context, CurrentTenant currentTenant)
    {
        //var claim = context.User.Claims.FirstOrDefault(c => c.Type == "tenant_id");
        var claim = context.User.Claims.FirstOrDefault(c => c.Type == TenantClaim.TenantId);
        if(claim is null)
            throw new UnauthorizedAccessException("Tenant ID claim is missing.");
        if(!Guid.TryParse(claim.Value, out var tenantId))
            throw new UnauthorizedAccessException("Invalid Tenant ID claim.");
        currentTenant.SetTenant(tenantId);
        await _request(context);
    }
}
