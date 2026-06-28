using Accounting.Core.Domain.Journals.Events;
using Accounting.Core.Domain.Journals.Rules;
using Accounting.Core.Domain.Journals.ValueObjects;
using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Primitives;
using BuildingBlocks.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals
{
    public class JournalEntry : EventSourcedAggregateRoot
    {
        public DateOnly Date { get; private set; }
        public JournalDescription Description { get; private set; } = default!;
        public JournalStatus Status { get; private set; } = default!;
        public JournalEntryType Type { get; private set; } = default!;
        public IReadOnlyList<JournalLine> Lines => _lines;

        private readonly List<JournalLine> _lines = new();

        private JournalEntry() { }

        public static JournalEntry Create(
             Guid id,
             DateOnly date,
             string description,
             int journalEntryTypeId)
        {
            var journalDesc = JournalDescription.Create(description);
            JournalEntryType journalEntryType = JournalEntryType.FromValue(journalEntryTypeId)!;

            var entry = new JournalEntry();
            entry.Raise(new JournalEntryCreated(id, date, journalDesc.Value, journalEntryType.Value));
            return entry;
        }

        public void AddLine(Guid accountId, decimal debit, decimal credit)
        {
            CheckRule(JournalRules.MustBeDraft(Status));

            CheckRule(JournalRules.ValidDebitCredit(debit, credit));

            var lineId = Guid.NewGuid();

            var @event = new JournalLineAdded(
                JournalEntryId: Id,
                LineId: lineId,
                AccountId: accountId,
                Debit: debit,
                Credit: credit
            );

            Raise(@event);
        }

        public void RemoveLine(Guid lineId)
        {
            CheckRule(JournalRules.MustBeDraft(Status));

            var line = _lines.FirstOrDefault(x => x.Id == lineId);

            if (line == null)
                throw new InvalidOperationException("Line not found");

            var @event = new JournalLineRemoved(
                JournalEntryId: Id,
                LineId: lineId
            );

            Raise(@event);
        }

        public void Post()
        {
            CheckRule(JournalRules.MustBeDraft(Status));

            CheckRule(JournalRules.MustHaveAtLeastOneLine(_lines.Count));

            EnsureBalanced();

             var @event = new JournalPosted(
                JournalEntryId: Id
            );

            Raise(@event);
        }

        public void EnsureBalanced()
        {
            var totalDebit = _lines.Sum(x => x.Debit);
            var totalCredit = _lines.Sum(x => x.Credit);

            CheckRule(JournalRules.MustBeBalanced(totalDebit, totalCredit));
        }


        #region Apply Methods (Event Handlers)

        public void Apply(JournalEntryCreated @event)
        {
            SetId(@event.JournalEntryId);
            Date = @event.Date;
            Description = (JournalDescription)@event.Description;
            Type = JournalEntryType.FromValue(@event.JournalEntryTypeId)!;
            Status = JournalStatus.Draft;
        }

        public void Apply(JournalLineAdded @event)
        {
            var line = new JournalLine(
                id: @event.LineId,
                accountId: @event.AccountId,
                debit: @event.Debit,
                credit: @event.Credit
            );

            _lines.Add(line);
        }

        public void Apply(JournalLineRemoved @event)
        {
            var line = _lines.FirstOrDefault(x => x.Id == @event.LineId);
            if (line != null)
                _lines.Remove(line);
        }

        public void Apply(JournalPosted @event)
        {
            Status = JournalStatus.Posted;
        }

        #endregion
    }
}
