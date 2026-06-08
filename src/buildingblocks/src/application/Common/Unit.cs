using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.Common;

/// <summary>
/// Using Unit as void type for Command with no return value, or Query with no result.
/// </summary>
public sealed record Unit
{
    public static readonly Unit Value = new();
    private Unit() { }
}