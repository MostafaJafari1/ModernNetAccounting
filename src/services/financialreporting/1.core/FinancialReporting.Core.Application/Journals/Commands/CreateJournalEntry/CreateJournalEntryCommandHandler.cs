using FinancialReporting.Core.Contracts.Journals;

namespace FinancialReporting.Core.Application.Journals.Commands.CreateJournalEntry;

public class CreateJournalEntryCommandHandler : BaseCommandHandler<CreateJournalEntryCommand, Unit>
{
    private readonly IJournalEntryWriteRepository _repository;

    public CreateJournalEntryCommandHandler(
        ILogger<CreateJournalEntryCommandHandler> logger,
        IJournalEntryWriteRepository repository)
        : base(logger)
    {
        _repository = repository;
    }

    protected override async Task<Result<Unit>> HandleAsync(
        CreateJournalEntryCommand command,
        CancellationToken cancellationToken)
    {
        var model = new JournalEntryWriteModel
        {
            JournalEntryId = command.JournalEntryId,
            Date = command.Date,
            Description = command.Description,
            JournalTypeId = command.JournalTypeId,
            JournalTypeName = command.JournalTypeName,
            Lines = command.Lines
                  .Select(l => new JournalEntryLineWriteModel
                  {
                      LineId = l.LineId,
                      AccountId = l.AccountId,
                      Debit = l.Debit,
                      Credit = l.Credit
                  })
                  .ToList()
        };

        await _repository.UpsertAsync(model, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}