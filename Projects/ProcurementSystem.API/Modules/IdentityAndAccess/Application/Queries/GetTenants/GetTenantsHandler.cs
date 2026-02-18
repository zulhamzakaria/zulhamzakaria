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

    public Task<Result<IReadOnlyCollection<GetTenantsDTO>>> Handle
        (GetTenantsQuery request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private GetTenantsDTO MapToDto(Tenant tenant)
    {

    }
}
