using Accounting.Core.Contracts.Accounts;
using Accounting.Core.Domain.Accounts;
using Accounting.Core.Domain.Accounts.Enums;
using Accounting.Core.Domain.Accounts.ValueObjects;
using Accounting.Infrastructure.Data.Sql.Write;
using BuildingBlocks.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using static FastExpressionCompiler.ExpressionCompiler;

namespace Accounting.Core.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler(
     ILogger<CreateAccountCommandHandler> logger,
     IMessageBus _bus,
     IAccountCommandRepository _accountRepository,
     AccountingCommandDbContext _context
    ) :
     BaseCommandHandler<CreateAccountCommand, Unit>(logger)
{
    protected override async Task<Result<Unit>> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {

        var accountName = AccountName.Create(command.Name);

        var accountCode = AccountCode.FromString(command.Code, Enumeration<AccountLevel>.FromValue((int)command.Level)!);

        var isCodeUnique = await _accountRepository.IsCodeUniqueAsync(accountCode, cancellationToken);
        if (!isCodeUnique)
        {
            throw new Exception("duplicate");
            //return Result.Fail<Unit>($"حسابی با کد '{command.Code}' از قبل در سیستم تعریف شده است.");
        }

        var account = Account.Create(
            accountCode,
            accountName,
            command.ParentId,
            Enumeration<AccountLevel>.FromValue((int)command.Level)!,
            Enumeration<AccountNature>.FromValue((int)command.Nature)!,
            command.IsPostable
        );

        await _accountRepository.AddAsync(account);
        await _context.SaveChangesAsync();


        return Result<Unit>.Success(Unit.Value);
    }
}
