using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialReporting.Core.Contracts.Journals;

public interface IJournalEntryReadRepository
{
    Task<JournalEntryReadModel?> GetByIdAsync(
        Guid journalEntryId,
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<JournalEntryReadModel> Items, long TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}