using System.ComponentModel.DataAnnotations;

namespace ProcurementSystem.API.SharedKernel.Security;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(ErrorMessage = "Jwt issuer is required")]
    public string Issuer { get; init; } = string.Empty;

    [Required(ErrorMessage = "Jwt Audience is required")]
    public string Audience { get; init; } = string.Empty;

    [Required(ErrorMessage = "Jwt SecretKey is required")]
    [MinLength(32, ErrorMessage = "Jwt SecretKey must be at least 32 characters long")]
    public string SecretKey { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; }
}
