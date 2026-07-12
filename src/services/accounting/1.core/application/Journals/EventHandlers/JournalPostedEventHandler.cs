using JasperFx.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Application.Journals.EventHandlers;

public class JournalPostedEventHandler(
    ILogger<JournalPostedEventHandler> logger,
    IMessageBus bus)
    : BaseEventHandler<JournalPosted>(logger)
{
    protected override async Task HandleAsync(
        JournalPosted domainEvent,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Mapping Domain Event to Integration Event for Journal: {JournalEntryId}", domainEvent.JournalEntryId);

        var integrationLines = domainEvent.Lines.Select(l => new JournalLineDto(
            LineId: l.LineId,
            AccountId: l.AccountId,
            Debit: l.Debit,
            Credit: l.Credit
        )).ToList();

        var integrationEvent = new JournalEntryPostedIntegrationEvent(
            JournalEntryId: domainEvent.JournalEntryId,
            Date: domainEvent.Date,
            Description: domainEvent.Description,
            JournalTypeId: domainEvent.JournalTypeId,
            JournalTypeName: domainEvent.JournalTypeName,
            Lines: integrationLines
        );

        await bus.PublishAsync(
             integrationEvent,
             new DeliveryOptions
             {
                 PartitionKey = integrationEvent.JournalEntryId.ToString()
             });


        logger.LogInformation("JournalEntryPostedIntegrationEvent published for Journal: {JournalEntryId}", domainEvent.JournalEntryId);
    }
}