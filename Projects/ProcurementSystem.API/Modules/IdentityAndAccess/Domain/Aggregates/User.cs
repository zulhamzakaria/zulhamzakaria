using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
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

    public User(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));
        Id = Guid.NewGuid();
        Username = username;
        SetPassword(password);
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
