using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Guards;
public sealed class GuardClause { }
public static class Guard
{
    public static readonly GuardClause Against = new GuardClause();
}