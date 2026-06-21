using Accounting.Core.Contracts.Journals;
using Accounting.Core.Domain.Journals;
using BuildingBlocks.Integrations.Marten.Repository;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.EventSourcing.Write.Journals
{
    public class JournalRepository : MartenEventStoreRepository<JournalEntry> , IJournalRepository
    {
        public JournalRepository(IDocumentSession session) : base(session) { }
    }
}
