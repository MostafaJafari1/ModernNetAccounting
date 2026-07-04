using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts.Enums;


/// <summary>
/// Defines the expected normal balance behavior of the account.
/// </summary>
public class AccountNature : Enumeration<AccountNature>
{
    public static readonly AccountNature Debtor = new(1, nameof(Debtor));     // بدهکار
    public static readonly AccountNature Creditor = new(2, nameof(Creditor)); // بستانکار
    public static readonly AccountNature DoubleNature = new(3, nameof(DoubleNature)); // دوطرفه / فاقد ماهیت خاص

    private AccountNature(int value, string name) : base(value, name)
    {
    }
}