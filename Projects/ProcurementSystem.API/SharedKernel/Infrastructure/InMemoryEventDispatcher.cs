using ProcurementSystem.API.SharedKernel.Application;
using ProcurementSystem.API.SharedKernel.Domain;
using System.Collections.Concurrent;
using System.Reflection;

namespace ProcurementSystem.API.SharedKernel.Infrastructure;

public sealed class InMemoryEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InMemoryEventDispatcher> _logger;
    private readonly ConcurrentDictionary
        <Type, (Type HandlerType, MethodInfo HandleMethod)> _methodCache = new();

    public InMemoryEventDispatcher(IServiceProvider serviceProvider, 
        ILogger<InMemoryEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct = default)
    {
        foreach (var e in events)
        {
            await DispatchEventAsync(e, ct);
        }
    }

    private async Task DispatchEventAsync(IDomainEvent domainEvent, CancellationToken ct)
    {
        var eventType = domainEvent.GetType();
        var (handlerType, handleMethod) = _methodCache.GetOrAdd(eventType, type =>
        {
            var hType = typeof(IDomainEventHandler<>).MakeGenericType(type);
            var method = hType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.Handle))!;
            return (hType, method);
        });

        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            try
            {
                var task = (Task)handleMethod.Invoke(handler, new object[] { domainEvent, ct })!;
                await task;

                _logger.LogDebug(
                    "Domain event {EventType} handled by {HandlerType}",
                    eventType.Name,
                    handler.GetType().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error handling domain event {EventType} with {HandlerType}",
                    eventType.Name,
                    handler.GetType().Name);

                // Continue to next handler
            }
        }
    }
}
