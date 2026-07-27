using FinancialReporting.Core.Contracts.Journals.IntegrationEvents;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialReporting.Core.Application.Mappings;

internal static class JournalMappingExtensions
{
    public static CreateGeneralLedgerCommand MapToCreateCommand(this JournalEntryPostedIntegrationEvent @event)
    {
        if (@event == null)
            return null;

        var mappedLines = @event.Lines?.Select(line => new JournalLineCommand(
            line.LineId,
            line.AccountId,
            line.Debit,
            line.Credit
        )).ToList() ?? new(); 

        return new CreateGeneralLedgerCommand(
            @event.JournalEntryId,
            @event.Date,
            @event.Description,
            @event.JournalTypeId,
            @event.JournalTypeName,
            mappedLines
        );
    }
}
