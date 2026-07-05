using Accounting.Core.Contracts.Accounts;
using Accounting.Core.Domain.Accounts;
using Accounting.Core.Domain.Accounts.ValueObjects;
using BuildingBlocks.Infrastructure.Data.Sql.Write;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Data.Sql.Write.Accounts;
public class AccountCommandRepository : BaseCommandRepository<Account, Guid>, IAccountCommandRepository
{
    public AccountCommandRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> IsCodeUniqueAsync(AccountCode code, CancellationToken cancellationToken = default)
    {
        return !await _dbSet.AnyAsync(a => a.Code == code, cancellationToken);
    }
}