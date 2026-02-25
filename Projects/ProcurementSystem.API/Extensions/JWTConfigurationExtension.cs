using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProcurementSystem.API.Extensions;

public static class JWTConfigurationExtension
{
    public static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration config)
    {

        //appsettings validations
        var jwtSection = config.GetSection("Jwt");
        if (!jwtSection.Exists())
            throw new Exception("JWT configuration section is missing in appsettings.");

        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var secretKey = jwtSection["SecretKey"];

        if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(secretKey))
            throw new InvalidOperationException("JWT configuration values (Issuer, Audience, SecretKey) must be provided in appsettings.");

        if(secretKey.Length < 32)
            throw new InvalidOperationException("JWT SecretKey must be at least 32 characters long for security reasons.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.FromMinutes(1) 
                };
            });

        return services;
    }
}
