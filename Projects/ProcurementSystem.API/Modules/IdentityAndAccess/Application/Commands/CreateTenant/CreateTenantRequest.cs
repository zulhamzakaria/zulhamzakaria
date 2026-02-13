using System.ComponentModel.DataAnnotations;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

public sealed record CreateTenantRequest(
    [Required, StringLength(50, MinimumLength = 3)] string TenantName,
    [Required, StringLength(50, MinimumLength = 3)] string TenantAlias);
