using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Primitives;

public abstract record DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        HappenedAt = DateTime.UtcNow;
    }

    public Guid Id { get; init; }

    public DateTime HappenedAt { get; init; }
}