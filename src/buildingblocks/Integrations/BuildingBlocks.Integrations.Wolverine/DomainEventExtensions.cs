using BuildingBlocks.Domain.Abstractions;
using Wolverine;

namespace BuildingBlocks.Integrations.Wolverine;

public static class DomainEventExtensions
{
    public static async Task PublishDomainEventsAsync(
        this IMessageBus bus,
        IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
            await bus.PublishAsync(@event);
    }
}