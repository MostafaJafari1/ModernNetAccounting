using Accounting.Core.Domain.Accounts.Enums;
using Accounting.Core.Domain.Accounts.ValueObjects;
using BuildingBlocks.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.Sql.Write;
public sealed class AccountNameConversion : ValueConverter<AccountName, string>
{
    public AccountNameConversion()
        : base(name => name.Value, value => AccountName.Create(value)) { }
}

public sealed class AccountCodeConversion : ValueConverter<AccountCode, string>
{
    public AccountCodeConversion()
        : base(
            code => code.Value,
            value => (AccountCode)Activator.CreateInstance(typeof(AccountCode), true, value)!)
    { }
}

public sealed class AccountLevelConversion : ValueConverter<AccountLevel, int>
{
    // تبدیل آبجکت به عدد و برعکس با استفاده از متدهای کلاس Enumeration
    public AccountLevelConversion()
        : base(level => level.Value, id => Enumeration<AccountLevel>.FromValue(id)) { }
}

public sealed class AccountNatureConversion : ValueConverter<AccountNature, int>
{
    public AccountNatureConversion()
        : base(nature => nature.Value, id => Enumeration<AccountNature>.FromValue(id)) { }
}