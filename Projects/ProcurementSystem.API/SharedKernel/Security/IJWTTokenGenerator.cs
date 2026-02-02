namespace ProcurementSystem.API.SharedKernel.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, Guid tenantId, string role);
}
