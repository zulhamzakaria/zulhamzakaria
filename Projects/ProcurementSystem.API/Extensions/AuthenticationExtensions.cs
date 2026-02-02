using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProcurementSystem.API.SharedKernel.Security;
using System.Text;

namespace ProcurementSystem.API.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>() ?? new JwtOptions();

        if (jwtOptions is null)
            throw new InvalidOperationException("JWT options are not configured properly.");
        if(string.IsNullOrWhiteSpace(jwtOptions.SecretKey))
            throw new InvalidOperationException("JWT SecretKey has not been configured.");


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                ClockSkew = TimeSpan.FromSeconds(30) //whats this? 
            };
        });
        return services;
    }
}
