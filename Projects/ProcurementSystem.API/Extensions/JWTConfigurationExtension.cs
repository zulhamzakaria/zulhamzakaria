using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProcurementSystem.API.SharedKernel.Security;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProcurementSystem.API.Extensions;

public static class JWTConfigurationExtension
{
    public static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration config)
    {

        services.AddOptions<JwtOptions>()
            .Bind(config.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtOptions = config
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()!;

        Validator.ValidateObject
            (jwtOptions, new ValidationContext(jwtOptions), validateAllProperties: true);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(1) 
                };
            });

        return services;
    }
}
