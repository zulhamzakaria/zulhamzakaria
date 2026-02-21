using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;

public sealed class User : BaseEntity, ITenantEntity
{
    public string Username { get; private set; } = string.Empty;
    private string _passwordHash;
    public UserRole Role { get; private set; }
    public Guid TenantId { get; private set; }
    Guid ITenantEntity.TenantId
    {
        get => TenantId;
        set => TenantId = value;
    }

    private User()
    {
        // EF Core
    }

    public static Result<User> Create
        (Guid tenantId, string username, string password, UserRole userRole)
    {
        List<Error> errors = new();
        if (tenantId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(tenantId)));
        if (string.IsNullOrWhiteSpace(username))
            errors.Add(CommonErrors.Required(nameof(username)));
        if (string.IsNullOrWhiteSpace(password))
            errors.Add(CommonErrors.Required(nameof(password)));

        if(errors.Count > 0)
            return Result<User>.Failure(errors);

        var user = new User()
        {
            TenantId = tenantId,
            Username = username,
            Role = userRole
        };

        user.SetPassword(password);

        return Result<User>.Success(user);
    }

    public static Result<User> CreateSuperAdmin(Guid tenantId)
    {
        User superAdmin = new()
        {
            TenantId = tenantId,
            Username = "SuperAdmin",
            Role = UserRole.SystemAdministrator,
        };
        superAdmin.SetPassword("abc123");

        return Result<User>.Success(superAdmin);
    }

    private void SetPassword(string password)
    {
        _passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, _passwordHash);
    }

    public void AssignRole(UserRole role)
    {
        Role = role;
    }
}
