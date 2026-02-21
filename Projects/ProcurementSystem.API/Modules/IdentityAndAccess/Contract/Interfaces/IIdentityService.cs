namespace ProcurementSystem.API.Modules.IdentityAndAccess.Contract.Interfaces;

public interface IIdentityService
{
     Task<Guid?> GetActiveTenantIdByAliasAsync(string alias);
}
