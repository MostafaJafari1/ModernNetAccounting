using FluentValidation;

namespace Accounting.Core.RequestResponse.Journals.Commands;

public class CreateJournalLineCommandValidator : AbstractValidator<CreateJournalLineCommand>
{
    public CreateJournalLineCommandValidator()
    {
        Validate();
    }

    private void Validate()
    {
        // AccountId validation
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account is required.");

        // Debit validation
        RuleFor(x => x.Debit)
            .GreaterThanOrEqualTo(0).WithMessage("Debit amount cannot be negative.");

        // Credit validation
        RuleFor(x => x.Credit)
            .GreaterThanOrEqualTo(0).WithMessage("Credit amount cannot be negative.");

        // Either Debit or Credit must have a value, not both
        RuleFor(x => x)
            .Must(x => x.Debit == 0 || x.Credit == 0)
            .WithMessage("A journal line cannot have both debit and credit values.")
            .Must(x => x.Debit > 0 || x.Credit > 0)
            .WithMessage("A journal line must have either a debit or credit value.");
    }
}