namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantByAlias;

public sealed record GetTenantByAliasDTO(
    Guid Id,
    string TenantName,
    string TenantAlias,
    string TenantStatus);
