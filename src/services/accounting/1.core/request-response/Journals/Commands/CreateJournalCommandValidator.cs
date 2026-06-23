using Accounting.Core.Resources.Constants;
using Accounting.Core.Resources.Messages;
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
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(ValidationMessages.Journal_DateRequired);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.Journal_DescriptionRequired)
            .MaximumLength(AppConstants.Journal.DescriptionMaxLength)
            .WithMessage(ValidationMessages.Journal_DescriptionMaxLength);

        RuleForEach(x => x.Lines)
            .SetValidator(new CreateJournalLineCommandValidator());
    }
}