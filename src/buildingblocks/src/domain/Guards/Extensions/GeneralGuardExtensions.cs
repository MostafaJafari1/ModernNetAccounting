using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class GeneralGuardExtensions
{
    public static T Must<T>(
        this GuardClause guard,
        T value,
        Func<T, bool> predicate,
        string message,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (!predicate(value))
        {
            throw new ArgumentException(message, parameterName);
        }
        return value;
    }
}