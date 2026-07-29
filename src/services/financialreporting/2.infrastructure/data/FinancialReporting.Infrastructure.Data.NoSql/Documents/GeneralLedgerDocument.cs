namespace FinancialReporting.Infrastructure.Data.NoSql.Documents;

public class GeneralLedgerDocument
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = default!;
    public int JournalTypeId { get; set; }
    public string JournalTypeName { get; set; } = default!;
    public List<GeneralLedgerLineDocument> Lines { get; set; } = [];
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GeneralLedgerLineDocument
{
    public Guid LineId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}