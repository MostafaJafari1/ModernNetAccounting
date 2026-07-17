using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Contracts.Application.CQRS.Event;
public interface IIntegrationEventHandler<TEvent>
    where TEvent : IIntegrationEvent
    {
        Task Handle(TEvent integrationEvent, CancellationToken cancellationToken);
    }