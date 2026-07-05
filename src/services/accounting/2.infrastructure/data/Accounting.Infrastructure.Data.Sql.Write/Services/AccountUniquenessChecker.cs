using Accounting.Core.Contracts.Accounts;
using Accounting.Core.Domain.Accounts.Services;
using Accounting.Core.Domain.Accounts.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Data.Sql.Write.Services;
public class AccountUniquenessChecker : IAccountUniquenessChecker
{
    private readonly IAccountCommandRepository _accountRepository;

    public AccountUniquenessChecker(IAccountCommandRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> IsCodeUniqueAsync(AccountCode code, CancellationToken cancellationToken = default)
    {
        return await _accountRepository.IsCodeUniqueAsync(code, cancellationToken);
    }
}