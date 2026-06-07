using System;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class StringGuardExtensions
{
    public static string NullOrEmpty(
        this GuardClause guard,
        string value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message ?? $"{parameterName} cannot be null or empty.", parameterName);
        }
        return value;
    }

    public static string MaxLength(
        this GuardClause guard,
        string value,
        int maxLength,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value.Length > maxLength)
        {
            throw new ArgumentException(message ?? $"{parameterName} cannot be longer than {maxLength} characters.", parameterName);
        }
        return value;
    }
}