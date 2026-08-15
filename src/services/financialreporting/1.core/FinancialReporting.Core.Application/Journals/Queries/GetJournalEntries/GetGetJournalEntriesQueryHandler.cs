using BuildingBlocks.Application.CQRS.Queries;
using BuildingBlocks.Common;
using FinancialReporting.Core.Contracts.Journals;
using FinancialReporting.Core.RequestResponse.Journals.Queries;
using Microsoft.Extensions.Logging;

namespace FinancialReporting.Core.Application.Journals.Queries.GetJournalEntries;

public sealed class GetJournalEntriesQueryHandler : BaseQueryHandler<GetJournalEntriesQuery, GetJournalEntriesResponse>
{
    private readonly IJournalEntryReadRepository _repository;

    public GetJournalEntriesQueryHandler(
        ILogger<GetJournalEntriesQueryHandler> logger,
        IJournalEntryReadRepository repository)
        : base(logger)
    {
        _repository = repository;
    }

    protected override async Task<Result<GetJournalEntriesResponse>> HandleAsync(
        GetJournalEntriesQuery query,
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _repository.GetPagedAsync(
            query.Page, query.PageSize, cancellationToken);

        var responseItems = items.Select(Map).ToList();

        var response = new GetJournalEntriesResponse(responseItems, totalCount, query.Page, query.PageSize);

        return Result<GetJournalEntriesResponse>.Success(response);
    }

    private static JournalEntryResponse Map(JournalEntryReadModel readModel)
    {
        return new JournalEntryResponse(
            readModel.Id,
            readModel.Date,
            readModel.Description,
            readModel.JournalTypeId,
            readModel.JournalTypeName,
            readModel.Lines
                .Select(l => new JournalEntryLineResponse(l.AccountId, l.AccountName, l.Debit, l.Credit))
                .ToList(),
            readModel.TotalDebit,
            readModel.TotalCredit,
            readModel.CreatedAt);
    }
}