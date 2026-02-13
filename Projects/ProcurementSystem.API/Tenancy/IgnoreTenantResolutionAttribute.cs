namespace ProcurementSystem.API.Tenancy;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreTenantResolutionAttribute : Attribute;