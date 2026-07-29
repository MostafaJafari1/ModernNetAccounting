using FinancialReporting.Core.Contracts.Journals;

namespace FinancialReporting.Core.Application.Journals.Commands.CreateGeneralLedger;

public class CreateGeneralLedgerCommandHandler(
     ILogger<CreateGeneralLedgerCommandHandler> logger,
     IGeneralLedgerWriteRepository repository
    ) :
     BaseCommandHandler<CreateGeneralLedgerCommand, Unit>(logger) 
{
    protected override async Task<Result<Unit>> HandleAsync(
        CreateGeneralLedgerCommand command,
        CancellationToken cancellationToken)
    {
        var model = new GeneralLedgerWriteModel
        {
            JournalEntryId = command.JournalEntryId,
            Date = command.Date,
            Description = command.Description,
            JournalTypeId = command.JournalTypeId,
            JournalTypeName = command.JournalTypeName,
            Lines = command.Lines
                  .Select(l => new GeneralLedgerLineWriteModel
                  {
                      LineId = l.LineId,
                      AccountId = l.AccountId,
                      Debit = l.Debit,
                      Credit = l.Credit
                  })
                  .ToList()
        };

        await repository.UpsertAsync(model, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
