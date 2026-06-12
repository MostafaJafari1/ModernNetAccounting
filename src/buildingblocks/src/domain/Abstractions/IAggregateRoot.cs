namespace BuildingBlocks.Domain.Abstractions;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();
    IReadOnlyCollection<IDomainEvent> DequeueDomainEvents();
    void ClearDomainEvents();
}
