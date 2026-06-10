using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BuildingBlocks.Common.Guards.Extensions;
public static class DateGuardExtensions
{
    public static DateTime InFuture(
        this GuardClause guard,
        DateTime value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value <= DateTime.UtcNow)
        {
            throw new ArgumentException(message ?? $"{parameterName} must be in the future.", parameterName);
        }
        return value;
    }
}