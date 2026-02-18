namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenants;

public sealed record GetTenantsDTO
    (Guid Id, string TenantName, string TenantAlias);
