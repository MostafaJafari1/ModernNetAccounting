using BuildingBlocks.Common;
using BuildingBlocks.Contracts.Application.CQRS.Queries;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application.CQRS.Queries;

public abstract class BaseQueryHandler<TQuery, TResult>
    : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    protected readonly ILogger Logger;

    protected BaseQueryHandler(ILogger logger)
    {
        Logger = logger;
    }

    public async Task<Result<TResult>> Handle(
        TQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation(
                "Started executing Query: {QueryType}",
                typeof(TQuery).Name);

            var result = await HandleAsync(query, cancellationToken);

            Logger.LogInformation(
                "Query executed successfully: {QueryType}",
                typeof(TQuery).Name);

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Unexpected error occurred while executing Query: {QueryType}",
                typeof(TQuery).Name);

            throw;
        }
    }

    protected abstract Task<Result<TResult>> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken);

    Task<Result<TResult>> IQueryHandler<TQuery, TResult>.HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        return HandleAsync(query, cancellationToken);
    }
}

