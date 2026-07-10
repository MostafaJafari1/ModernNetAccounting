namespace Accounting.Core.RequestResponse.Journals.Commands.PostJournal;

public class PostJournalCommandValidator : AbstractValidator<PostJournalCommand>
{
    public PostJournalCommandValidator()
    {
        Validate();
    }

    private void Validate()
    {
        RuleFor(x => x.JournalEntryId)
            .NotEmpty()
            .WithMessage("Requird.");
    }
}
