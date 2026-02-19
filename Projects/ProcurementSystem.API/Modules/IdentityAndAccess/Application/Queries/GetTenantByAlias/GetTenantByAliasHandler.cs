using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantByAlias;

public sealed class GetTenantByAliasHandler :
     IRequestHandler<GetTenantByAliasQuery, Result<GetTenantByAliasDTO>>
{

    private readonly IADbContext _dbContext;

    public GetTenantByAliasHandler(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTenantByAliasDTO>> Handle
        (GetTenantByAliasQuery request, CancellationToken cancellationToken = default)
    {
        var tenant = await _dbContext.Tenants
            .Where(t => t.TenantAlias == request.Alias)
            .Select(t => new GetTenantByAliasDTO
            (t.Id,
             t.TenantName,
             t.TenantAlias,
             t.TenantStatus.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        if (tenant is null)
            return Result<GetTenantByAliasDTO>.Failure
                (CommonErrors.NotFound(nameof(Tenant), $"TenantAlias:{request.Alias}"));

        return Result<GetTenantByAliasDTO>.Success(tenant);
    }
}
