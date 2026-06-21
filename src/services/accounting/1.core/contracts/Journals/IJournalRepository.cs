using Accounting.Core.Domain.Journals;
using BuildingBlocks.Integrations.Marten.Repository;

namespace Accounting.Core.Contracts.Journals;

public interface IJournalRepository : IMartenEventStoreRepository<JournalEntry> { }
