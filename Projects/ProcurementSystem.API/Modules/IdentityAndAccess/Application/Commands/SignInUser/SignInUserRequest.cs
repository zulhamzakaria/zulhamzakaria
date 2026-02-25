using System.ComponentModel.DataAnnotations;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

public sealed record SignInUserRequest(
    [Required] string TenantAlias,
    [Required] string Username,
    [Required] string Password);
