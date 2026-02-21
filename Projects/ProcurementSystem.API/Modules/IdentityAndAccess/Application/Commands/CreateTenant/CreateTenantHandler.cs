using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

public sealed class CreateTenantHandler : IRequestHandler<CreateTenantCommand, Result<Guid>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateTenantHandler
        (ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken = default)
    {
        var tenantExists = await _tenantRepository.IsTenantExistsAsync(request.TenantName, cancellationToken);
        if (tenantExists)
            return Result<Guid>.Failure(CommonErrors.DuplicateEntry(nameof(Tenant), request.TenantName));

        tenantExists = await _tenantRepository.IsTenantExistsAsync(request.TenantAlias, cancellationToken);
        if (tenantExists)
            return Result<Guid>.Failure(CommonErrors.DuplicateEntry(nameof(Tenant), request.TenantAlias));

        var newTenant = Tenant.Create(request.TenantAlias, request.TenantName);
        if (newTenant.IsFailure)
        {
            return Result<Guid>.Failure(newTenant.Errors);
        }
        _tenantRepository.AddTenant(newTenant.Value!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(newTenant.Value!.Id);
    }
}
