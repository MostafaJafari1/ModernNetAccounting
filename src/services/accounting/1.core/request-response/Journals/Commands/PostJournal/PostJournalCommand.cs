namespace Accounting.Core.RequestResponse.Journals.Commands.PostJournal;
public class PostJournalCommand : ICommand<Unit>
{
    public Guid JournalEntryId { get; set; }
}