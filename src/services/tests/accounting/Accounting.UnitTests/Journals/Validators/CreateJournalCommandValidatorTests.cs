using FluentAssertions;
using FluentValidation.TestHelper;
using Accounting.Core.RequestResponse.Journals.Commands;

namespace Accounting.UnitTests.Journals.Validators;

public class CreateJournalCommandValidatorTests
{
    private readonly CreateJournalCommandValidator _validator;

    public CreateJournalCommandValidatorTests()
    {
        _validator = new CreateJournalCommandValidator();
    }

    private static CreateJournalCommand ValidCommand() => new()
    {
        Date = DateOnly.FromDateTime(DateTime.Today),
        Description = "Test journal entry",
        Lines = new List<CreateJournalLineCommand>
        {
            new() { AccountId = Guid.NewGuid(), Debit = 1000, Credit = 0 },
            new() { AccountId = Guid.NewGuid(), Debit = 0,    Credit = 1000 }
        }
    };

    //Date Tests

    [Fact]
    public void Date_WhenEmpty_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Date = default;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Journal date is required.");
    }

    [Fact]
    public void Date_WhenProvided_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Date = DateOnly.FromDateTime(DateTime.Today);

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Date);
    }

    //Description Tests
    [Fact]
    public void Description_WhenEmpty_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Description = string.Empty;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("Journal description is required.");
    }

    [Fact]
    public void Description_WhenNull_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Description = null!;

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Description_WhenExceeds500Characters_ShouldHaveValidationError()
    {
        var command = ValidCommand();
        command.Description = new string('A', 501);

        _validator.TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("Description cannot exceed 500 characters.");
    }

    [Fact]
    public void Description_WhenExactly500Characters_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Description = new string('A', 500);

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    //Lines Tests

    [Fact]
    public void Lines_WhenEmpty_ShouldNotHaveStructuralValidationError()
    {
        var command = ValidCommand();
        command.Lines = new List<CreateJournalLineCommand>();

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Lines);
    }

    [Fact]
    public void Lines_WhenNull_ShouldNotHaveValidationError()
    {
        var command = ValidCommand();
        command.Lines = null!;

        _validator.TestValidate(command)
            .ShouldNotHaveValidationErrorFor(x => x.Lines);
    }

    //Full Valid Command

    [Fact]
    public void ValidCommand_ShouldPassAllValidations()
    {
        var command = ValidCommand();

        var result = _validator.TestValidate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidCommand_WithUnbalancedLines_ShouldStillPassValidator()
    {
        var command = ValidCommand();
        command.Lines = new List<CreateJournalLineCommand>
        {
            new() { AccountId = Guid.NewGuid(), Debit = 1000, Credit = 0 },
            new() { AccountId = Guid.NewGuid(), Debit = 0,    Credit = 500 }
        };

        var result = _validator.TestValidate(command);

        result.IsValid.Should().BeTrue();
    }
}