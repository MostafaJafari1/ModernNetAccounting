using Accounting.Core.RequestResponse.Accounts.Enums;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Contracts.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Accounts.Commands;

public record CreateAccountCommand(
    string Code,
    string Name,
    Guid? ParentId,
    AccountLevel Level,
    AccountNature Nature,
    bool IsPostable) : ICommand<Unit>;