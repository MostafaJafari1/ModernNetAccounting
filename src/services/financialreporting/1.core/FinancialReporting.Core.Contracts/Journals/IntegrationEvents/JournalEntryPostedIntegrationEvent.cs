using BuildingBlocks.Contracts.Application.CQRS.Event;
using BuildingBlocks.Domain.Abstractions;
using Wolverine.Attributes;

namespace FinancialReporting.Core.Contracts.Journals.IntegrationEvents;

[MessageIdentity("journal-entry-posted")]
public record JournalEntryPostedIntegrationEvent(
    Guid Id,
    Guid JournalEntryId,
    DateOnly Date,
    string Description,
    int JournalTypeId,
    string JournalTypeName,
    List<JournalLineDto> Lines,
    DateTime HappenedAt) : IIntegrationEvent;

public record JournalLineDto(
    Guid LineId,
    Guid AccountId,
    decimal Debit,
    decimal Credit);