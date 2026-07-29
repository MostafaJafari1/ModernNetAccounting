namespace FinancialReporting.Core.Contracts.Journals;

public interface IGeneralLedgerWriteRepository
{
    Task UpsertAsync(GeneralLedgerWriteModel model, CancellationToken cancellationToken);
}