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

        // Each line validation
        RuleForEach(x => x.Lines)
            .SetValidator(new CreateJournalLineCommandValidator());
    }
}