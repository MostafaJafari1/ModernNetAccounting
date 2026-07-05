using Accounting.Core.Domain.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.Sql.Write.Accounts.ValueConverters;
public sealed class AccountCodeConverter : ValueConverter<AccountCode, string>
{
    public AccountCodeConverter()
        : base(
            code => code.Value,
            value => AccountCode.FromDatabase(value))
    { }
}