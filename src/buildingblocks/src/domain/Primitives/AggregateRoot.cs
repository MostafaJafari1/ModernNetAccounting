using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Primitives;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    // لیست داخلی رویدادهای دامنه
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() { }

    protected AggregateRoot(Guid id) : base(id) { }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() =>
        _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyCollection<IDomainEvent> DequeueDomainEvents()
    {
        var events = GetDomainEvents();
        ClearDomainEvents();
        return events;
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}