using Accounting.Core.RequestResponse.Journals.Dtos;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Contracts.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

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
