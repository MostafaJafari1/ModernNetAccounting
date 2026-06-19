using FluentValidation.TestHelper;
using Accounting.Core.RequestResponse.Journals.Commands;
using Xunit;
using FluentAssertions;

namespace Accounting.UnitTests.Journals.Validators;


public class CreateJournalLineCommandValidatorTests
{
    private readonly CreateJournalLineCommandValidator _validator;

    public CreateJournalLineCommandValidatorTests()
    {
        _validator = new CreateJournalLineCommandValidator();
    }

    private static CreateJournalLineCommand ValidCommand() => new()
    {
        AccountId = Guid.NewGuid(),
        Debit = 1000,
        Credit = 0
    };

    //AccountId Tests

    [Fact]
    public void AccountId_WhenEmpty_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.AccountId = Guid.Empty;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.AccountId);
    }

    [Fact]
    public void AccountId_WhenProvided_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.AccountId = Guid.NewGuid();

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.AccountId);
    }

    //Debit Tests

    [Fact]
    public void Debit_WhenNegative_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Debit = -1;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Debit);
    }

    [Fact]
    public void Debit_WhenZero_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Debit = 0;

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Debit);
    }

    [Fact]
    public void Debit_WhenPositive_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Debit = 1000;

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Debit);
    }

    //Credit Tests

    [Fact]
    public void Credit_WhenNegative_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Credit = -1;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Credit);
    }

    [Fact]
    public void Credit_WhenZero_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Credit = 0;

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Credit);
    }

    [Fact]
    public void Credit_WhenPositive_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Credit = 1000;
        command.Debit = 0;

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Credit);
    }

    //Full Valid Command

    [Fact]
    public void ValidCommand_WithDebit_ShouldPassAllValidations()
    {
        var command = new CreateJournalLineCommand
        {
            AccountId = Guid.NewGuid(),
            Debit = 1000,
            Credit = 0
        };

        _validator.TestValidate(command)
            .IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidCommand_WithCredit_ShouldPassAllValidations()
    {
        var command = new CreateJournalLineCommand
        {
            AccountId = Guid.NewGuid(),
            Debit = 0,
            Credit = 1000
        };

        _validator.TestValidate(command)
            .IsValid.Should().BeTrue();
    }
}