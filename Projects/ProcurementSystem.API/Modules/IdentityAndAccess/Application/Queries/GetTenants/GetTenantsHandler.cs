using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenants;

public sealed class GetTenantsHandler :
    IRequestHandler<GetTenantsQuery, Result<IReadOnlyCollection<GetTenantsDTO>>>
{
    private readonly IADbContext _dbContext;

    public GetTenantsHandler(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IReadOnlyCollection<GetTenantsDTO>>> Handle
        (GetTenantsQuery request, CancellationToken cancellationToken = default)
    {

        IQueryable<Tenant> query = _dbContext.Tenants.AsNoTracking();

        if (request.TenantStatus is not null)
            query = query.Where(t => t.TenantStatus == request.TenantStatus);


        var tenants = await query
            .Select(t => new GetTenantsDTO(
                t.Id,
                t.TenantName,
                t.TenantAlias))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<GetTenantsDTO>>.Success(tenants);
    }

}
