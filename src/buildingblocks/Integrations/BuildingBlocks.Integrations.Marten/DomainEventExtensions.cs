using BuildingBlocks.Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Wolverine;
using Wolverine.Marten;

namespace BuildingBlocks.Integrations.Marten;

/// <summary>
/// Provides extension methods for publishing domain events through Wolverine,
/// supporting both non-transactional (immediate) dispatching and transactional
/// (Outbox-based) dispatching patterns.
/// </summary>
public static class DomainEventExtensions
{
    /// <summary>
    /// Publishes domain events globally without transaction awareness.
    /// </summary>
    /// <remarks>
    /// CAUTION: This method dispatches events immediately. Do not use this inside 
    /// Command Handlers that rely on the PostgreSQL Outbox pattern, because events 
    /// will be sent even if the database transaction fails or rolls back.
    /// </remarks>
    /// <param name="bus">The global Wolverine message bus.</param>
    /// <param name="events">The collection of domain events to publish.</param>
    /// <param name="logger">The logger instance to record immediate dispatching.</param>
    public static async Task PublishDomainEventsAsync(
        this IMessageBus bus,
        IEnumerable<IDomainEvent> events,
        ILogger logger)
    {
        logger.LogWarning("Dispatching domain events immediately via global IMessageBus (Non-Transactional/Outbox bypassed).");

        foreach (var @event in events)
        {
            logger.LogDebug("Publishing event {EventName} immediately.", @event.GetType().Name);
            await bus.PublishAsync(@event);
        }
    }

    /// <summary>
    /// Publishes domain events transactionally through the Marten Outbox.
    /// </summary>
    /// <remarks>
    /// RECOMMENDED: Use this method inside Command Handlers where the <see cref="IMartenOutbox"/>
    /// has been enrolled with the active <c>IDocumentSession</c> (via <c>outbox.Enroll(session)</c>).
    /// Events published here are staged in the Wolverine envelope tables (e.g. 
    /// wolverine_incoming_envelopes for local queues) and are only persisted atomically 
    /// when <c>SaveChangesAsync</c> is called on the enrolled session. This guarantees that 
    /// events are never lost, and are never sent if the underlying transaction fails or rolls back.
    /// </remarks>
    /// <param name="bus">The Marten-integrated Wolverine outbox, enrolled with the current document session.</param>
    /// <param name="events">The collection of domain events to stage in the Outbox.</param>
    /// <param name="logger">The logger instance to record staging activity.</param>
    public static async Task PublishDomainEventsAsync(
        this IMartenOutbox bus,
        IEnumerable<IDomainEvent> events,
        ILogger logger)
    {
        logger.LogInformation("Staging domain events into the Marten/Wolverine Outbox (transactional dispatch).");

        foreach (var @event in events)
        {
            logger.LogDebug("Enlisting event {EventName} into the current Outbox transaction context.", @event.GetType().Name);
            await bus.PublishAsync(@event);
        }
    }
}