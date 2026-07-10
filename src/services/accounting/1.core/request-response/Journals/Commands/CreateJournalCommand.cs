namespace Accounting.Core.RequestResponse.Journals.Commands
{
    public class CreateJournalCommand : ICommand<Unit>
    {
        public DateOnly Date { get; set; }
        public string Description { get; set; } = default!;
        public JournalEntryTypeDto JournalEntryType { get; set; } = JournalEntryTypeDto.General;
        public List<CreateJournalLineCommand> Lines { get; set; } = new();
    }
}
