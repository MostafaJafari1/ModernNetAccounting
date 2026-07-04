using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Primitives.AggregateRoots;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    // لیست داخلی رویدادهای دامنه
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() { }

    protected AggregateRoot(Guid id) : base(id) { }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() =>
        _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    //Use For Soft Delete
    public bool IsDeleted { get; private set; } = false;

    //Use For Soft Delete
    public virtual void Delete()
    {
        if (IsDeleted) return; 
        this.IsDeleted = true;
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}