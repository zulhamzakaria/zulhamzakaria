namespace ProcurementSystem.API.Modules.IdentityAndAccess.Contract;

public interface IIdentityService
{
     Task<Guid?> GetActiveTenantIdByAliasAsync(string alias);
}
