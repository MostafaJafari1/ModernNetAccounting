using BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Contracts.Application.CQRS.Event;
public interface IEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}