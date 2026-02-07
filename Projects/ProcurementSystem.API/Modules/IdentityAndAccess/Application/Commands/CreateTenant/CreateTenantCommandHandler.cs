using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure.Repositories;
using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Application.Commands.CreateTenant;

public sealed class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<Guid>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateTenantCommandHandler
        (ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken = default)
    {


        var tenant = Tenant.Create(request.TenantAlias, request.TenantName);
        if (tenant.IsFailure)
        {
            return Result<Guid>.Failure(tenant.Errors);
        }
        _tenantRepository.AddTenant(tenant.Value!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(tenant.Value!.Id);
    }
}
