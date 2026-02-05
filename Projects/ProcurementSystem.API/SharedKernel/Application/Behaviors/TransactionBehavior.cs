using ProcurementSystem.API.SharedKernel.Application.Messaging;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.SharedKernel.Application.Behaviors;

public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle
        (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        var response = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}
