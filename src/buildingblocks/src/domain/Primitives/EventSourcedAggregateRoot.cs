using BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Primitives;

public abstract class EventSourcedAggregateRoot : AggregateRoot, IEventSourcedAggregateRoot
{
    public long Version { get; protected set; }

    public void ReplayEvent(IDomainEvent domainEvent)
    {
        When(domainEvent);
        Version++;
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        When(domainEvent);
        RaiseDomainEvent(domainEvent);
    }

    private void When(IDomainEvent domainEvent)
        => ((dynamic)this).Apply((dynamic)domainEvent);
}
