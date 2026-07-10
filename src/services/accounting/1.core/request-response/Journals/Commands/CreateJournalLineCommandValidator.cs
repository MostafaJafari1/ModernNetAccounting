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
            .NotEmpty()
            .WithMessage(ValidationMessages.JournalLine_AccountRequired);

        // Debit validation
        RuleFor(x => x.Debit)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationMessages.JournalLine_DebitCannotBeNegative);

        // Credit validation
        RuleFor(x => x.Credit)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationMessages.JournalLine_CreditCannotBeNegative);

        // Either Debit or Credit must have a value, not both
        RuleFor(x => x)
            .Must(x => x.Debit == 0 || x.Credit == 0)
            .WithMessage(ValidationMessages.JournalLine_BothDebitAndCredit)
            .Must(x => x.Debit > 0 || x.Credit > 0)
            .WithMessage(ValidationMessages.JournalLine_MustHaveDebitOrCredit);
    }
}