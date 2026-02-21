using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Domain;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

public sealed class Tenant : BaseEntity
{
    private const int MinAliasLength = 3;
    private const int MaxAliasLength = 50;
    private const int MinNameLength = 3;
    private const int MaxNameLength = 100;
    public string TenantAlias { get; private set; } = string.Empty;
    public string TenantName { get; private set; } = string.Empty;
    public TenantStatus TenantStatus { get; private set; } = TenantStatus.Active;
    private Tenant()
    {
        // EF Core
    }

    public static Result<Tenant> Create(string tenantAlias, string tenantName)
    {
        List<Error> errors = new();
        if (string.IsNullOrWhiteSpace(tenantAlias))
            errors.Add(CommonErrors.Required(nameof(tenantAlias)));
        else if (tenantAlias.Length < MinAliasLength || tenantAlias.Length > MaxAliasLength)
            errors.Add(CommonErrors.InvalidLength(nameof(tenantAlias), MinAliasLength, MaxAliasLength));
        if (string.IsNullOrWhiteSpace(tenantName))
            errors.Add(CommonErrors.Required(nameof(tenantName)));
        else if (tenantName.Length < MinNameLength || tenantName.Length > MaxNameLength)
            errors.Add(CommonErrors.InvalidLength(nameof(tenantName), MinNameLength, MaxNameLength));
        if (errors.Any())
            return Result<Tenant>.Failure(errors);
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            TenantAlias = tenantAlias,
            TenantName = tenantName
        };
        return Result<Tenant>.Success(tenant);
    }

    public Result Activate()
    {
        if(TenantStatus == TenantStatus.Active)
            return Result.Failure(CommonErrors.InvalidStatusChange(TenantStatus.ToString()));
        TenantStatus = TenantStatus.Active;
        return Result.Success();
    }
    public Result Deactivate()
    {
        if (TenantStatus == TenantStatus.Inactive)
            return Result.Failure(CommonErrors.InvalidStatusChange(TenantStatus.ToString()));
        TenantStatus = TenantStatus.Inactive;
        return Result.Success();
    }
    public Result Suspend()
    {
        if(TenantStatus == TenantStatus.Suspended)
            return Result.Failure(CommonErrors.InvalidStatusChange(TenantStatus.ToString()));
        TenantStatus = TenantStatus.Suspended;
        return Result.Success();
    }

}
