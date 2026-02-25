using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;
using ProcurementSystem.API.SharedKernel.Security;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.SignInUser;

public sealed class SignInUserHandler : IRequestHandler<SignInUserCommand, Result<string>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IADbContext _context;
    public SignInUserHandler
        (ITenantRepository tenantRepository, IADbContext context, IJwtTokenGenerator tokenGenerator)
    {
        _tenantRepository = tenantRepository;
        _context = context;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<Result<string>> Handle(SignInUserCommand command, CancellationToken cancellationToken = default)
    {
        //get tenantId
        var tenantId = await _tenantRepository.GetTenantIdByAliasAsync
            (command.TenantAlias, cancellationToken);
        if (tenantId is null)
            return Result<string>.Failure(CommonErrors.NotFound(nameof(Tenant), $"Alias:{command.TenantAlias}"));

        //validate user
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Username.ToUpper() == command.Username.ToUpper(), cancellationToken);

        if(user is null)
            return Result<string>.Failure(CommonErrors.NotFound(nameof(User), $"Username:{command.Username} for {command.TenantAlias}"));

        if (!user.VerifyPassword(command.Password))
            return Result<string>.Failure(CommonErrors.UnauthorizedAccess("Invalid username or password."));

        var token = _tokenGenerator.GenerateToken(user.Id, user.TenantId, user.Role.ToString());

        return Result<string>.Success(token);

    }
}
