namespace FinancialReporting.Core.Contracts.Journals;

public interface IJournalEntryWriteRepository
{
    Task UpsertAsync(JournalEntryWriteModel model, CancellationToken cancellationToken);
}