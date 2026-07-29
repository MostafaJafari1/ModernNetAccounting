using BuildingBlocks.Application.Common;
using BuildingBlocks.Contracts.Application.CQRS.Commands;

namespace FinancialReporting.Core.RequestResponse.Journals.Commands;

public class CreateGeneralLedgerCommand
(
    Guid JournalEntryId,
    DateOnly Date,
    string Description,
    int JournalTypeId,
    string JournalTypeName,
    List<JournalLineCommand> Lines) : ICommand<Unit>
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime HappenedAt { get; } = DateTime.Now;

    public Guid JournalEntryId { get; } = JournalEntryId;
    public DateOnly Date { get; } = Date;
    public string Description { get; } = Description;
    public int JournalTypeId { get; } = JournalTypeId;
    public string JournalTypeName { get; } = JournalTypeName;
    public List<JournalLineCommand> Lines { get; } = Lines;
}
public record JournalLineCommand(
    Guid LineId,
    Guid AccountId,
    decimal Debit,
    decimal Credit);