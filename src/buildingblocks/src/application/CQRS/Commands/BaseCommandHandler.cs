using BuildingBlocks.Application.Common;
using BuildingBlocks.Contracts.Application.CQRS.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                Logger.LogInformation("Started executing Command: {CommandType}", typeof(TCommand).Name);

                var result = await HandleAsync(command, cancellationToken);

                stopwatch.Stop();

                if (result.IsSuccess)
                    Logger.LogInformation(
                        "Command executed successfully: {CommandType} in {ElapsedMs}ms",
                        typeof(TCommand).Name,
                        stopwatch.ElapsedMilliseconds);
                else
                    Logger.LogWarning(
                        "Command failed: {CommandType} - {Error} in {ElapsedMs}ms",
                        typeof(TCommand).Name,
                        result.Error.Message,
                        stopwatch.ElapsedMilliseconds);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                Logger.LogError(ex,
                    "Unexpected error occurred while executing Command: {CommandType} in {ElapsedMs}ms",
                    typeof(TCommand).Name,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }

        protected abstract Task<Result<TResult>> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken);

      
    }
}
