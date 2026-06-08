using BuildingBlocks.Application.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.CQRS.Command
{

    public abstract class BaseCommandHandler<TCommand, TResult>
        : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected readonly ILogger Logger;

        protected BaseCommandHandler(ILogger logger)
        {
            Logger = logger;
        }

        public async Task<Result<TResult>> Handle(
            TCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation(
                    "Started executing Command: {CommandType}",
                    typeof(TCommand).Name);

                var result = await HandleAsync(command, cancellationToken);

                Logger.LogInformation(
                    "Command executed successfully: {CommandType}",
                    typeof(TCommand).Name);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,
                    "Unexpected error occurred while executing Command: {CommandType}",
                    typeof(TCommand).Name);

                throw;
            }
        }

        protected abstract Task<Result<TResult>> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken);
    }
}
