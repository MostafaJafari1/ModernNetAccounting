using BuildingBlocks.Common;
using BuildingBlocks.Contracts.Application.CQRS.Commands;
using BuildingBlocks.Contracts.Application.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Wolverine;

namespace BuildingBlocks.Integrations.Wolverine;
public static class MessageBusExtensions
{
    public static async Task<Result<TResult>> SendQueryAsync<TResult>(
        this IMessageBus bus,
        IQuery<TResult> query)
        => await bus.InvokeAsync<Result<TResult>>(query);

    public static async Task<Result<TResult>> SendCommandAsync<TResult>(
        this IMessageBus bus,
        ICommand<TResult> command)
        => await bus.InvokeAsync<Result<TResult>>(command);
}