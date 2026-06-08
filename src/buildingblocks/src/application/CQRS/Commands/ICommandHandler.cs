using BuildingBlocks.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.CQRS.Command;

public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}