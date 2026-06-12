using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime HappenedAt { get; } = DateTime.UtcNow;
}