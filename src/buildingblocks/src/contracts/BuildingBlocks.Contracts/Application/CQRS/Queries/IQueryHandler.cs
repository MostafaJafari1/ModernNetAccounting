using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Contracts.Application.CQRS.Queries;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken);
}