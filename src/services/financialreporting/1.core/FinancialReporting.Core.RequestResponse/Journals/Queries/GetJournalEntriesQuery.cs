using BuildingBlocks.Contracts.Application.CQRS.Queries;

namespace FinancialReporting.Core.RequestResponse.Journals.Queries;
public sealed record GetJournalEntriesQuery(int Page, int PageSize) : IQuery<GetJournalEntriesResponse>;

