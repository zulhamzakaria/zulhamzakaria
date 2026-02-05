using ProcurementSystem.API.SharedKernel.Application.Messaging;

namespace ProcurementSystem.API.SharedKernel.Infrastructure.Messaging;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
    {
        var handler = _serviceProvider
            .GetRequiredService<IRequestHandler<TRequest, TResponse>>();

        var behaviors = _serviceProvider
            .GetServices<IPipelineBehavior<TRequest, TResponse>>()
            .ToArray();

        if (behaviors.Length == 0)
        {
            return await handler.Handle(request, cancellationToken);
        }

        RequestHandlerDelegate<TResponse> pipeline =
            () => handler.Handle(request, cancellationToken);

        for (int i = behaviors.Length - 1; i >= 0; i--)
        {
            var behavior = behaviors[i];
            var next = pipeline;
            pipeline = () => behavior.Handle(request, next, cancellationToken);
        }

        return await pipeline();
    }
}
