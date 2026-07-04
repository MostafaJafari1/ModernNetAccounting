using Accounting.Core.Resources.Constants;
using Accounting.Core.Resources.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Accounts.Commands;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        Validate();
    }

    private void Validate()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(ValidationMessages.Account_CodeRequired)
            .MaximumLength(AppConstants.Account.CodeMaxLength)
            .WithMessage(ValidationMessages.Account_CodeMaxLength);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.Account_NameRequired)
            .MaximumLength(AppConstants.Account.NameMaxLength)
            .WithMessage(ValidationMessages.Account_NameMaxLength);

        RuleFor(x => x.Level)
            .IsInEnum()
            .WithMessage(ValidationMessages.Account_InvalidLevel);

        RuleFor(x => x.Nature)
            .IsInEnum()
            .WithMessage(ValidationMessages.Account_InvalidNature);

    }
}