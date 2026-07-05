using Accounting.Core.Domain.Accounts.Enums;
using BuildingBlocks.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting.Infrastructure.Data.Sql.Write.Accounts.ValueConverters;
public sealed class AccountNatureConverter : ValueConverter<AccountNature, int>
{
    public AccountNatureConverter()
        : base(nature => nature.Value, id => Enumeration<AccountNature>.FromValue(id)!) { }
}