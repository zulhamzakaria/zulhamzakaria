using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantById;

public sealed class GetTenantByIdHandler : IRequestHandler<GetTenantByIdQuery, Result<GetTenantByIdDTO>>
{
    private readonly IADbContext _dbContext;
    public GetTenantByIdHandler(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<GetTenantByIdDTO>> Handle
        (GetTenantByIdQuery request, CancellationToken cancellationToken = default)
    {
        var tenant = await _dbContext.Tenants
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new GetTenantByIdDTO
            (t.Id, t.TenantName, t.TenantAlias, t.TenantStatus.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        if (tenant is null)
            return Result<GetTenantByIdDTO>.Failure
                (CommonErrors.NotFound(nameof(Tenant), $"TenantId: {request.Id}"));

        return Result<GetTenantByIdDTO>.Success(tenant);
    }
}
