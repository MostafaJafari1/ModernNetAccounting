using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Events
{
    public sealed record JournalLineRemoved(
        Guid JournalEntryId,
        Guid LineId
    ) : DomainEvent;
}
