using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.ValueObjects;

public sealed record JournalDescription : ValueObject
{
    public string Value { get; init; }

    private JournalDescription(string value)
    {
        Value = value;
    }

    public static JournalDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));
        }

        if (value.Length > 500)
        {
            throw new ArgumentException("Description cannot exceed 500 characters.", nameof(value));
        }

        return new JournalDescription(value.Trim());
    }

    public static implicit operator string(JournalDescription description) => description.Value;

    public static explicit operator JournalDescription(string value) => Create(value);
}
