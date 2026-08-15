using FinancialReporting.Core.Contracts.Journals;
using FinancialReporting.Infrastructure.Data.NoSql.Documents;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialReporting.Infrastructure.Data.NoSql.Repositories;

public sealed class JournalEntryReadRepository : IJournalEntryReadRepository
{
    private readonly IMongoCollection<JournalEntryDocument> _collection;

    public JournalEntryReadRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<JournalEntryDocument>("JournalEntries");
    }

    public async Task<JournalEntryReadModel?> GetByIdAsync(
        Guid journalEntryId,
        CancellationToken cancellationToken)
    {
        var document = await _collection
            .Find(x => x.Id == journalEntryId)
            .FirstOrDefaultAsync(cancellationToken);

        return document is null ? null : Map(document);
    }

    public async Task<(IReadOnlyList<JournalEntryReadModel> Items, long TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var filter = FilterDefinition<JournalEntryDocument>.Empty;

        var totalCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var documents = await _collection
            .Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        var items = documents.Select(Map).ToList();

        return (items, totalCount);
    }

    private static JournalEntryReadModel Map(JournalEntryDocument document)
    {
        return new JournalEntryReadModel
        {
            Id = document.Id,
            Date = document.Date,
            Description = document.Description,
            JournalTypeId = document.JournalTypeId,
            JournalTypeName = document.JournalTypeName,
            Lines = document.Lines
                .Select(l => new JournalEntryLineReadModel
                {
                    AccountId = l.AccountId,
                    AccountName = l.AccountName,
                    Debit = l.Debit,
                    Credit = l.Credit
                })
                .ToList(),
            TotalDebit = document.TotalDebit,
            TotalCredit = document.TotalCredit,
            CreatedAt = document.CreatedAt
        };
    }
}