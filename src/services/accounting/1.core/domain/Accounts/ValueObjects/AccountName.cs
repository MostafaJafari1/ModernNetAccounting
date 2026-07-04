using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts.ValueObjects;

public record AccountName
{
    public string Value { get; init; }

    private AccountName(string value)
    {
        Value = value;
    }

    public static AccountName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account name cannot be empty.");

        if (value.Length > 250)
            throw new ArgumentException("Account name cannot exceed 200 characters.");

        return new AccountName(value.Trim());
    }

    public static implicit operator string(AccountName accountName) => accountName?.Value ?? string.Empty;
}