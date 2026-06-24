using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals;

public sealed class JournalStatus : Enumeration<JournalStatus>
{
    public static readonly JournalStatus Draft = new(1, nameof(Draft));
    public static readonly JournalStatus Posted = new(2, nameof(Posted));

    private JournalStatus(int value, string name) : base(value, name)
    {
    }
}