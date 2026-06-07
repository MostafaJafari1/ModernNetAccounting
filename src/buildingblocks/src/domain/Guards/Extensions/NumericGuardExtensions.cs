using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class NumericGuardExtensions
{
    public static int OutOfRange(
        this GuardClause guard,
        int value,
        int min,
        int max,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                message ?? $"{parameterName} must be between {min} and {max}.");
        }
        return value;
    }

    public static int Positive(
        this GuardClause guard,
        int value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName} cannot be negative.");
        }
        return value;
    }
}