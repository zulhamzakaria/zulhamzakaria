using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Contract.Interfaces;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application;

public class IdentityService : IIdentityService
{
    private readonly IADbContext _dbContext;

    public IdentityService(IADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid?> GetActiveTenantIdByAliasAsync(string alias)
        => await _dbContext.Tenants
        .AsNoTracking()
        .Where(t => EF.Functions.ILike(t.TenantAlias, alias))
        .Where(t => t.TenantStatus == TenantStatus.Active)
        .Select(t => t.Id)
        .FirstOrDefaultAsync();
}
