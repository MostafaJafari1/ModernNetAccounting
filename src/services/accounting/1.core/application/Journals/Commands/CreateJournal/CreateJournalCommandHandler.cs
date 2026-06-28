using Accounting.Core.Contracts.Journals;
using Accounting.Core.Domain.Journals;
using Accounting.Core.RequestResponse.Journals.Commands;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Common;
using BuildingBlocks.Integrations.Wolverine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Wolverine;

namespace Accounting.Core.Application.Journals.Commands.CreateJournal
{
    public class CreateJournalCommandHandler(
        ILogger<CreateJournalCommandHandler> logger,
        IJournalRepository _journalRepository, IMessageBus _bus) :
        BaseCommandHandler<CreateJournalCommand, Unit>(logger)
    {
        protected override async Task<Result<Unit>> HandleAsync(
            CreateJournalCommand command,
            CancellationToken cancellationToken)
        {
            var journalId = Guid.NewGuid();

            var journal = JournalEntry.Create(
                id: journalId,
                date: command.Date,
                description: command.Description,
                journalEntryTypeId: (int)command.JournalEntryType
            );

            foreach (var line in command.Lines)
            {
                journal.AddLine(
                    accountId: line.AccountId,
                    debit: line.Debit,
                    credit: line.Credit
                );
            }

            var events = journal.GetDomainEvents();

            await _journalRepository.AppendEventsAsync(
                journal.Id,
                events: events,
                cancellationToken: cancellationToken);

            await _bus.PublishDomainEventsAsync(events);

            journal.ClearDomainEvents();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
