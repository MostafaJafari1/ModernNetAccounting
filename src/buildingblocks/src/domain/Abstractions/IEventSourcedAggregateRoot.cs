using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Abstractions;

public interface IEventSourcedAggregateRoot : IAggregateRoot
{
    long Version { get; }
    void ReplayEvent(IDomainEvent domainEvent);
}