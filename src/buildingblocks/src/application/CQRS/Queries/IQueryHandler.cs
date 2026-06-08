using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.CQRS.Query;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken);
}