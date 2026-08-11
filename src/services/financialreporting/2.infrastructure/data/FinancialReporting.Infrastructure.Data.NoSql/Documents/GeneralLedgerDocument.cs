using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialReporting.Infrastructure.Data.NoSql.Documents;

public class GeneralLedgerDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    // DateOnly is serialized as string "YYYY-MM-DD"
    [BsonRepresentation(BsonType.String)]
    public DateOnly Date { get; set; }

    public string Description { get; set; } = default!;

    public int JournalTypeId { get; set; }

    public string JournalTypeName { get; set; } = default!;

    public List<GeneralLedgerLineDocument> Lines { get; set; } = [];

    // Decimal128 preserves precision for financial data
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalDebit { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalCredit { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }
}

public class GeneralLedgerLineDocument
{
    [BsonRepresentation(BsonType.String)]
    public Guid LineId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid AccountId { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Debit { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Credit { get; set; }
}