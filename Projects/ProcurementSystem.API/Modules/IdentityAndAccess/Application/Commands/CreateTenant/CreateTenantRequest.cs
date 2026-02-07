namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

public sealed record CreateTenantRequest(string TenantName, string TenantAlias);
