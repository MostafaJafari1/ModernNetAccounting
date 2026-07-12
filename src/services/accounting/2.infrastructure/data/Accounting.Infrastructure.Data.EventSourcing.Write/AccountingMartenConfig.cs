using Accounting.Core.Domain.Journals;
using Accounting.Core.Domain.Journals.Events;
using BuildingBlocks.Domain.Abstractions;
using ImTools;
using JasperFx.Events;
using JasperFx.Events.Projections;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.EventSourcing.Write
{
    public static class AccountingMartenConfig
    {
        public static void Configure(StoreOptions options, AccountingMartenSettings accountingSettings)
        {

            options.Events.StreamIdentity = StreamIdentity.AsGuid;

            //options.Projections.Snapshot<JournalEntry>(SnapshotLifecycle.Inline);

            options.DatabaseSchemaName = accountingSettings.SchemaName;
            options.Events.DatabaseSchemaName = accountingSettings.EventsSchemaName;

            options.Events.AddEventTypes(
                typeof(JournalEntryCreated).Assembly.GetExportedTypes()
                    .Where(t => typeof(IDomainEvent).IsAssignableFrom(t)
                                && t.IsClass && !t.IsAbstract));
        }
    }
}
