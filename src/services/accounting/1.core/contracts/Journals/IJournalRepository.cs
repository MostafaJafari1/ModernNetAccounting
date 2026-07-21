using Accounting.Core.Domain.Journals;
using BuildingBlocks.Integrations.Marten.Repository;

namespace Accounting.Core.Contracts.Journals;

public interface IJournalRepository
{
    Task AppendEventsAsync(
       Guid streamId,
       IEnumerable<object> events,
       long? expectedVersion = null,
       CancellationToken cancellationToken = default);

    Task<JournalEntry> GetByIdAsync(
        Guid streamId,
        long? version = null,
        CancellationToken cancellationToken = default);
}
