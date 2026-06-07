using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class ObjectGuardExtensions
{
    public static T NotNull<T>(
        this GuardClause guard,
        T value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName, message ?? $"{parameterName} cannot be null.");
        }
        return value;
    }
}