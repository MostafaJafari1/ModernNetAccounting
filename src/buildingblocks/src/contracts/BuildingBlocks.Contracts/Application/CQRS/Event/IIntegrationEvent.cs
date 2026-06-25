using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Contracts.Application.CQRS.Event;
public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime HappenedAt { get; }
}