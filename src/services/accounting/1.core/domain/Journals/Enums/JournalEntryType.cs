using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Enums;

public sealed class JournalType : Enumeration<JournalType>
{
    /// <summary>
    /// Represents standard day-to-day business transactions such as sales, purchases, and payroll.
    /// </summary>
    public static readonly JournalType General = new(1, nameof(General));

    /// <summary>
    /// Used for partial corrections, value modifications, or required period-end financial adjustments.
    /// </summary>
    public static readonly JournalType Adjustment = new(2, nameof(Adjustment));

    /// <summary>
    /// Used to completely reverse and neutralize a previously posted incorrect journal entry.
    /// </summary>
    public static readonly JournalType Reversal = new(3, nameof(Reversal));

    /// <summary>
    /// Represents the opening balance journal entry, carrying forward balances from the previous fiscal year.
    /// </summary>
    public static readonly JournalType Opening = new(4, nameof(Opening));

    /// <summary>
    /// Represents the closing journal entry used to close temporary revenue and expense accounts at year-end.
    /// </summary>
    public static readonly JournalType Closing = new(5, nameof(Closing));

    private JournalType(int value, string name) : base(value, name)
    {
    }
}