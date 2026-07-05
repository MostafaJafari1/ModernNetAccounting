using BuildingBlocks.Infrastructure.Data.Write;

namespace Accounting.Core.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler(
     ILogger<CreateAccountCommandHandler> logger,
     IAccountCommandRepository _accountRepository,
     IAccountUniquenessChecker _accountUniquenessChecker,
     IUnitOfWork _unitOfWork
    ) :
     BaseCommandHandler<CreateAccountCommand, Unit>(logger)
{
    protected override async Task<Result<Unit>> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var accountLevel = Enumeration<AccountLevel>.FromValue((int)command.Level)!;
        var accountNature = Enumeration<AccountNature>.FromValue((int)command.Nature)!;

        var accountCode = AccountCode.FromString(command.Code, accountLevel);

        var isUnique = await _accountUniquenessChecker.IsCodeUniqueAsync(accountCode, cancellationToken);
        if (!isUnique)
            return Result<Unit>.Failure(
            new Error(
                ValidationMessages.Account_DuplicateCode,
                ValidationMessages.Account_DuplicateCodeMessage,
                ErrorType.Conflict
            ));


        var account = Account.Create(
            accountCode,
            AccountName.Create(command.Name),
            command.ParentId,
            accountLevel,
            accountNature,
            command.IsPostable
        );

        await _accountRepository.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
