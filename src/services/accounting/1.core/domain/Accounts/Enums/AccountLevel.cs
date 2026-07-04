using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts.Enums;

/// <summary>
/// Defines the standard hierarchical levels in Chart of Accounts.
/// </summary>
public class AccountLevel : Enumeration<AccountLevel>
{
    public static readonly AccountLevel Group = new(1, nameof(Group));
    public static readonly AccountLevel General = new(2, nameof(General));
    public static readonly AccountLevel Subsidiary = new(3, nameof(Subsidiary));
    public static readonly AccountLevel Detail = new(4, nameof(Detail));

    private AccountLevel(int value, string name) : base(value, name)
    {
    }
}