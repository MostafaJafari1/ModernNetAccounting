using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialReporting.Core.Contracts.Journals;

public sealed class JournalEntryReadModel
{
    public Guid Id { get; init; }

    public DateOnly Date { get; init; }

    public string Description { get; init; } = string.Empty;

    public int JournalTypeId { get; init; }

    public string JournalTypeName { get; init; } = string.Empty;

    public IReadOnlyList<JournalEntryLineReadModel> Lines { get; init; } = [];

    public decimal TotalDebit { get; init; }

    public decimal TotalCredit { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}

public sealed class JournalEntryLineReadModel
{
    public Guid AccountId { get; init; }

    public string AccountName { get; init; } = string.Empty;

    public decimal Debit { get; init; }

    public decimal Credit { get; init; }
}