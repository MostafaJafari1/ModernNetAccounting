namespace BuildingBlocks.Domain.Abstractions;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime HappenedAt { get; }
}