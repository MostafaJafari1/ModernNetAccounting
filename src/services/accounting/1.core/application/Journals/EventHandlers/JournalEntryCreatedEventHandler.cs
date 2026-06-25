using Accounting.Core.Contracts.Journals.IntegrationEvents;
using Accounting.Core.Domain.Journals.Events;
using BuildingBlocks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Accounting.Core.Application.Journals.EventHandlers;
public class JournalEntryCreatedEventHandler(
    ILogger<JournalEntryCreatedEventHandler> logger,
    IMessageBus _bus)
    : BaseEventHandler<JournalEntryCreated>(logger)
{
    protected override async Task HandleAsync(
        JournalEntryCreated domainEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new JournalEntryCreatedIntegrationEvent(
            JournalEntryId: domainEvent.JournalEntryId,
            Date: domainEvent.Date,
            Description: domainEvent.Description,
            HappenedAt: DateTime.UtcNow
        );

        await _bus.PublishAsync(integrationEvent);
    }
}