using System;
using System.Collections.Generic;
using System.Text;

public sealed class GuardClause { }
public static class Guard
{
    public static readonly GuardClause Against = new GuardClause();
}