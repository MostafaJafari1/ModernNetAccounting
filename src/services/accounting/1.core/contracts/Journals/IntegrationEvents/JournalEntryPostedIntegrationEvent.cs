using BuildingBlocks.Contracts.Application.CQRS.Event;
using System;
using System.Collections.Generic;
using System.Text;
using Wolverine.Attributes;

namespace Accounting.Core.Contracts.Journals.IntegrationEvents;

[MessageIdentity("journal-entry-posted")]
public record JournalEntryPostedIntegrationEvent(
    Guid JournalEntryId,
    DateOnly Date,
    string Description,
    int JournalTypeId,
    string JournalTypeName,
    List<JournalLineDto> Lines) : IIntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime HappenedAt { get; } = DateTime.Now;
}

public record JournalLineDto(
    Guid LineId,
    Guid AccountId,
    decimal Debit,
    decimal Credit);