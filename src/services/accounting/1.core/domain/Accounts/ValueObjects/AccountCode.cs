using Accounting.Core.Domain.Accounts.Enums;
using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts.ValueObjects;

// Defined as an immutable record for built-in value equality
public record AccountCode : ValueObject
{
    public string Value { get; init; }

    protected AccountCode() { }

    private AccountCode(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Factory method to validate and create an account code.
    /// </summary>
    public static AccountCode FromString(string code, AccountLevel level)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Account code cannot be empty.");

        var cleanedCode = code.Trim();

        //Must be numeric
        if (!long.TryParse(cleanedCode, out _))
            throw new ArgumentException("Account code must contain numbers only.");

        //Validate code length based on accounting level
        int expectedLength = level switch
        {
            _ when level == AccountLevel.Group => 1,
            _ when level == AccountLevel.General => 2,
            _ when level == AccountLevel.Subsidiary => 4,
            _ when level == AccountLevel.Detail => 6,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };

        if (cleanedCode.Length != expectedLength)
            throw new ArgumentException($"Account code for level '{level}' must be exactly {expectedLength} digits long.");

        return new AccountCode(cleanedCode);
    }

    public static implicit operator string(AccountCode accountCode) => accountCode?.Value;

    public override string ToString() => Value;
}
