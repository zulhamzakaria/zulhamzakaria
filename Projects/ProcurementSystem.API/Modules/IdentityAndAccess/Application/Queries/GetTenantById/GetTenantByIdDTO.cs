namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Queries.GetTenantById;

public sealed record GetTenantByIdDTO
    (Guid Id, 
    string CompanyName,
    string CompanyAlias,
    string TenantStatus);
