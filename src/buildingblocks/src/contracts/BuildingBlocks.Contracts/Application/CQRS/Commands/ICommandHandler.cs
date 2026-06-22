using BuildingBlocks.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Contracts.Application.CQRS.Commands;

public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}