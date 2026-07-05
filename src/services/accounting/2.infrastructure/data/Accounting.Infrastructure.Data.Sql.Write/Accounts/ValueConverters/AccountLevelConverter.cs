using Accounting.Core.Domain.Accounts.Enums;
using BuildingBlocks.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.Sql.Write.Accounts.ValueConverters;
public sealed class AccountLevelConverter : ValueConverter<AccountLevel, int>
{
    public AccountLevelConverter()
        : base(level => level.Value, id => Enumeration<AccountLevel>.FromValue(id)!) { }
}