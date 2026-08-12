using FinancialReporting.Core.Contracts.Journals;
using FinancialReporting.Infrastructure.Data.NoSql.Documents;
using MongoDB.Driver;

namespace FinancialReporting.Infrastructure.Data.NoSql.Repositories;

public class JournalEntryWriteRepository : IJournalEntryWriteRepository
{
    private readonly IMongoCollection<JournalEntryDocument> collection;

    public JournalEntryWriteRepository(IMongoDatabase database)
    {
        collection = database.GetCollection<JournalEntryDocument>("JournalEntries");
    }

    public async Task UpsertAsync(JournalEntryWriteModel model, CancellationToken cancellationToken)
    {
        var document = new JournalEntryDocument
        {
            Id = model.JournalEntryId,
            Date = model.Date,
            Description = model.Description,
            JournalTypeId = model.JournalTypeId,
            JournalTypeName = model.JournalTypeName,
            Lines = model.Lines
                .Select(l => new JournalEntryLineDocument
                {
                    LineId = l.LineId,
                    AccountId = l.AccountId,
                    Debit = l.Debit,
                    Credit = l.Credit
                })
                .ToList(),
            TotalDebit = model.Lines.Sum(l => l.Debit),
            TotalCredit = model.Lines.Sum(l => l.Credit),
            CreatedAt = DateTime.UtcNow
        };

        var filter = Builders<JournalEntryDocument>.Filter.Eq(x => x.Id, document.Id);

        await collection.ReplaceOneAsync(
            filter,
            document,
            new ReplaceOptions { IsUpsert = true },
            cancellationToken);
    }
}