using BuildingBlocks.Contracts.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Journals.Commands
{
    public class CreateJournalLineCommand : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
