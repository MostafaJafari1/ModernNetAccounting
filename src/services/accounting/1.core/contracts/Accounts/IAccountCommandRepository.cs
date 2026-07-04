using Accounting.Core.Domain.Accounts;
using Accounting.Core.Domain.Accounts.ValueObjects;
using BuildingBlocks.Contracts.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Contracts.Accounts;
public interface IAccountCommandRepository : IBaseCommandRepository<Account, Guid>
{
    Task<bool> IsCodeUniqueAsync(AccountCode code, CancellationToken cancellationToken = default);
}