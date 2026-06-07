using System.Runtime.CompilerServices;

namespace BuildingBlocks.Domain.Guards.Extensions;
public static class GuidGuardExtensions
{
    public static Guid NotEmpty(
        this GuardClause guard,
        Guid value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message ?? $"{parameterName} cannot be empty.", parameterName);
        }
        return value;
    }
}