namespace BuildingBlocks.Domain.Abstractions;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
