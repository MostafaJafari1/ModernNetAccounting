using BuildingBlocks.Application.Common;
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
        object query)
        => await bus.InvokeAsync<Result<TResult>>(query);

    public static async Task<Result<TResult>> SendCommandAsync<TResult>(
        this IMessageBus bus,
        object command)
        => await bus.InvokeAsync<Result<TResult>>(command);
}