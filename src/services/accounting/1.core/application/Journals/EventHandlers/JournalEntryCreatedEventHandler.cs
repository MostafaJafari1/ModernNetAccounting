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
        //await _bus.PublishAsync(integrationEvent);
    }
}