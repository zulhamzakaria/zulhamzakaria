namespace ProcurementSystem.API.SharedKernel.Application.Messaging;

public interface IMediator
{
    Task<TResponse> Send<TRequest, TResponse>
        (TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>;
}
