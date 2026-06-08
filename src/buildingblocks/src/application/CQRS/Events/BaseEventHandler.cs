using BuildingBlocks.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application.CQRS.Events;

public abstract class BaseEventHandler<TEvent>
    : IEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    protected readonly ILogger Logger;

    protected BaseEventHandler(ILogger logger)
    {
        Logger = logger;
    }

    // Wolverine discovers this method
    public async Task Handle(
        TEvent domainEvent,
        CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation(
                "Started processing Event: {EventType} - Id: {EventId}",
                typeof(TEvent).Name,
                domainEvent.Id);

            await HandleAsync(domainEvent, cancellationToken);

            Logger.LogInformation(
                "Event processed successfully: {EventType} - Id: {EventId}",
                typeof(TEvent).Name,
                domainEvent.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Error occurred while processing Event: {EventType} - Id: {EventId}",
                typeof(TEvent).Name,
                domainEvent.Id);

            throw;
        }
    }

    protected abstract Task HandleAsync(
        TEvent domainEvent,
        CancellationToken cancellationToken);

   
}