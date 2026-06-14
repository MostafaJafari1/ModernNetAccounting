using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.EventSourcing.Write;

public class AccountingMartenSettings
{
    public string SchemaName { get; init; } = "accounting";
    public string EventsSchemaName { get; init; } = "accounting_events";
}
