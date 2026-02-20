namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.SearchTenantByName;

public sealed record SearchTenantByNameDTO
    (Guid Id, string TenantName, string TenantAlias, string TenantStatus);
