
namespace FinancialReporting.Core.RequestResponse.Journals.Queries;

public sealed record GetJournalEntriesResponse(
                      IReadOnlyList<JournalEntryResponse> Items,
                      long TotalCount,
                      int Page,
                      int PageSize);

public sealed record JournalEntryResponse(
                     Guid Id,
                     DateOnly Date,
                     string Description,
                     int JournalTypeId,
                     string JournalTypeName,
                     IReadOnlyList<JournalEntryLineResponse> Lines,
                     decimal TotalDebit,
                     decimal TotalCredit,
                     DateTimeOffset CreatedAt);

public sealed record JournalEntryLineResponse(
                        Guid AccountId,
                        string AccountName,
                        decimal Debit,
                        decimal Credit);
