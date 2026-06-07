using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class CollectionGuardExtensions
{
    public static IEnumerable<T> NotEmpty<T>(
        this GuardClause guard,
        IEnumerable<T> value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value == null || !value.Any())
        {
            throw new ArgumentException(message ?? $"{parameterName} cannot be null or empty.", parameterName);
        }
        return value;
    }
}