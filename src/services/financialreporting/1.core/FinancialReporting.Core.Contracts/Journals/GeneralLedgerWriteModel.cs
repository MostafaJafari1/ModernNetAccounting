namespace FinancialReporting.Core.Contracts.Journals;

public class GeneralLedgerWriteModel
{
    public Guid JournalEntryId { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = default!;
    public int JournalTypeId { get; set; }
    public string JournalTypeName { get; set; } = default!;
    public List<GeneralLedgerLineWriteModel> Lines { get; set; } = [];
}

public class GeneralLedgerLineWriteModel
{
    public Guid LineId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}