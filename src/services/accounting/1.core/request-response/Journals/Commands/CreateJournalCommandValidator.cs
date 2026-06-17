using FluentValidation;

namespace Accounting.Core.RequestResponse.Journals.Commands;

public class CreateJournalCommandValidator : AbstractValidator<CreateJournalCommand>
{
    public CreateJournalCommandValidator()
    {
        Validate();
    }

    private void Validate()
    {
        // Date validation
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Journal date is required.");

        // Description validation
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Journal description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        // Lines collection validation
        RuleFor(x => x.Lines)
            .NotEmpty().WithMessage("Journal must have at least one line.");

        // Each line validation
        RuleForEach(x => x.Lines)
            .SetValidator(new CreateJournalLineCommandValidator());

        // Balance validation (Total Debit must equal Total Credit)
        RuleFor(x => x.Lines)
            .Must(lines => lines.Sum(l => l.Debit) == lines.Sum(l => l.Credit))
            .WithMessage("Total debit must equal total credit.");
    }
}