using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Events
{
    public sealed record JournalLineAdded(
        Guid JournalEntryId,
        Guid LineId,
        Guid AccountId,
        decimal Debit,
        decimal Credit
    ) : DomainEvent;
}
