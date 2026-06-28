using Accounting.Core.Domain.Journals;
using Accounting.Core.Domain.Journals.ValueObjects;
using Accounting.Core.RequestResponse.Journals.Dtos;
using BuildingBlocks.Domain.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.UnitTests.Journals.Domain
{
    public class JournalEntryTests
    {
        #region Fixtures
        private readonly Guid _journalId = Guid.NewGuid();
        private readonly Guid _accountId = Guid.NewGuid();
        private readonly DateOnly _testDate = DateOnly.FromDateTime(DateTime.Today);
        #endregion

        [Fact]
        public void Create_WithValidParameters_ShouldInitializeJournalInDraftStatus()
        {
            // Act
            var desc = "Opening Entry";
            var journal = JournalEntry.Create(_journalId, _testDate, desc, (int)JournalEntryTypeDto.General);

            // Assert
            journal.Id.Should().Be(_journalId);
            journal.Date.Should().Be(_testDate);
            journal.Description.Value.Should().Be(desc);
            journal.Status.Should().Be(JournalStatus.Draft);
            journal.Type.Should().Be(JournalEntryType.General);
            journal.Lines.Should().BeEmpty();
        }


        [Fact]
        public void AddLine_WhenJournalIsDraftAndValidAmounts_ShouldAddLineSuccessfully()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Add Line Test", (int)JournalEntryTypeDto.General);

            // Act
            journal.AddLine(_accountId, debit: 1000, credit: 0);

            // Assert
            journal.Lines.Should().HaveCount(1);
            var addedLine = journal.Lines.First();
            addedLine.AccountId.Should().Be(_accountId);
            addedLine.Debit.Should().Be(1000);
            addedLine.Credit.Should().Be(0);
        }

        [Fact]
        public void AddLine_WhenJournalIsAlreadyPosted_ShouldThrowException()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Post Entry", (int)JournalEntryTypeDto.General);
            var account1 = Guid.NewGuid();
            var account2 = Guid.NewGuid();
            journal.AddLine(account1, debit: 500, credit: 0);
            journal.AddLine(account2, debit: 0, credit: 500);
            journal.Post();

            // Act
            Action action = () => journal.AddLine(_accountId, debit: 100, credit: 0);
            
            // Assert
            action.Should().Throw<BusinessRuleValidationException>();
        }

        [Fact]
        public void RemoveLine_WhenLineExists_ShouldRemoveLineFromCollection()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Remove Line", (int)JournalEntryTypeDto.General);
            journal.AddLine(_accountId, debit: 2000, credit: 0);
            var addedLineId = journal.Lines.First().Id;

            // Act
            journal.RemoveLine(addedLineId);

            // Assert
            journal.Lines.Should().BeEmpty();
        }


        [Fact]
        public void RemoveLine_WhenLineDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Remove Line which not exist", (int)JournalEntryTypeDto.General);
            var fakeLineId = Guid.NewGuid();

            // Act
            Action action = () => journal.RemoveLine(fakeLineId);

            // Assert
            action.Should().Throw<InvalidOperationException>()
                  .WithMessage("Line not found");
        }


        [Fact]
        public void Post_WhenJournalIsBalanced_ShouldChangeStatusToPosted()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Balanced Entry", (int)JournalEntryTypeDto.General);
            var bimehAccount = Guid.NewGuid();
            var bankAccount = Guid.NewGuid();

            journal.AddLine(bimehAccount, debit: 1500, credit: 0); 
            journal.AddLine(bankAccount, debit: 0, credit: 1500);  

            // Act
            journal.Post();

            // Assert
            journal.Status.Should().Be(JournalStatus.Posted);
        }

        [Fact]
        public void Post_WhenJournalIsUnbalanced_ShouldThrowException()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Unbalanced Entry", (int)JournalEntryTypeDto.General);
            journal.AddLine(Guid.NewGuid(), debit: 2000, credit: 0);
            journal.AddLine(Guid.NewGuid(), debit: 0, credit: 1100); 

            // Act
            Action action = () => journal.Post();

            // Assert
            action.Should().Throw<BusinessRuleValidationException>();
            journal.Status.Should().Be(JournalStatus.Draft); 
        }

        [Fact]
        public void Post_WhenJournalHasNoLines_ShouldThrowException()
        {
            // Arrange
            var journal = JournalEntry.Create(_journalId, _testDate, "Empty Entry", (int)JournalEntryTypeDto.General);

            // Act
            Action action = () => journal.Post();

            // Assert
            action.Should().Throw<BusinessRuleValidationException>();
        }
    }
}
