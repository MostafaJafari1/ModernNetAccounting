using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Events
{
    public sealed record JournalEntryCreated(
        Guid JournalEntryId,
        DateOnly Date,
        string Description,
        int JournalTypeId) : DomainEvent;
}
