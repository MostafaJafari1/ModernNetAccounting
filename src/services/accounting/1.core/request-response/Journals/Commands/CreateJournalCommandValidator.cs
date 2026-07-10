namespace Accounting.Core.RequestResponse.Journals.Commands;

public class CreateJournalCommandValidator : AbstractValidator<CreateJournalCommand>
{
    public CreateJournalCommandValidator()
    {
        Validate();
    }

    private void Validate()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(ValidationMessages.Journal_DateRequired);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.Journal_DescriptionRequired)
            .MaximumLength(AppConstants.Journal.DescriptionMaxLength)
            .WithMessage(ValidationMessages.Journal_DescriptionMaxLength);

        RuleFor(x => x.JournalEntryType)
            .IsInEnum()
            .WithMessage(ValidationMessages.Journal_InvalidEntryType);

        RuleForEach(x => x.Lines)
            .SetValidator(new CreateJournalLineCommandValidator());
    }
}