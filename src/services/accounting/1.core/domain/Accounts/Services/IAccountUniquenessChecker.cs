using Accounting.Core.Domain.Accounts.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts.Services;

public interface IAccountUniquenessChecker
{
    Task<bool> IsCodeUniqueAsync(AccountCode code, CancellationToken cancellationToken = default);
}