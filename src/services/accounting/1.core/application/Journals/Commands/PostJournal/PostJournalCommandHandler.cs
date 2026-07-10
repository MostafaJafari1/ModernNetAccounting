using Accounting.Core.RequestResponse.Journals.Commands.PostJournal;
using BuildingBlocks.Integrations.Marten;
using Marten;
using Wolverine.Marten;
using static Accounting.Core.Resources.Constants.AppConstants;

namespace Accounting.Core.Application.Journals.Commands.PostJournal;

public class PostJournalCommandHandler(
    ILogger<PostJournalCommandHandler> _logger,
    IJournalRepository _journalRepository, IMartenOutbox outbox, IDocumentSession session) :
    BaseCommandHandler<PostJournalCommand, Unit>(_logger)
{
    protected override async Task<Result<Unit>> HandleAsync(
        PostJournalCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing PostJournalCommand for JournalEntryId: {JournalEntryId}", command.JournalEntryId);
      
        //ℹ Enroll a Marten document session into the outbox'd sender
        outbox.Enroll(session);

        var journal = await _journalRepository.GetByIdAsync(command.JournalEntryId);
        if (journal == null)
        {
            return Result<Unit>.Failure(new Error(
                "JournalEntry.NotFound",
                $"Journal entry with ID '{command.JournalEntryId}' was not found.",
                ErrorType.Failure));
        }


        journal.Post();

        var events = journal.GetDomainEvents();

        await _journalRepository.AppendEventsAsync(
                                    journal.Id,
                                    events: events,
                                    cancellationToken: cancellationToken);

        await outbox.PublishDomainEventsAsync(events, Logger);

        journal.ClearDomainEvents();

        return Result<Unit>.Success(Unit.Value);
    }
}
