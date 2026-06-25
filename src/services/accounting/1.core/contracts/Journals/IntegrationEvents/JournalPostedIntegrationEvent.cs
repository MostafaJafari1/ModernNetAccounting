using BuildingBlocks.Contracts.Application.CQRS.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Contracts.Journals.IntegrationEvents;

public record JournalPostedIntegrationEvent(
    Guid JournalEntryId,
    DateTime HappenedAt
) : IIntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();
}
