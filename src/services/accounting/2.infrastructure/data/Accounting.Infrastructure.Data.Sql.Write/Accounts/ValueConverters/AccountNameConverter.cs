using Accounting.Core.Domain.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.Sql.Write.Accounts.ValueConverters;

public sealed class AccountNameConverter : ValueConverter<AccountName, string>
{
    public AccountNameConverter()
        : base(name => name.Value, value => AccountName.Create(value)) { }
}
