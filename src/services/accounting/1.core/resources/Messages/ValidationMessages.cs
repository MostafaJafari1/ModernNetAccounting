namespace Accounting.Core.Resources.Messages;

public static class ValidationMessages
{
    #region Journal
    // Journal
    public static readonly string Journal_DateRequired = "Journal date is required.";
    public static readonly string Journal_DescriptionRequired = "Journal description is required.";
    public static readonly string Journal_DescriptionMaxLength = "Description cannot exceed 500 characters.";

    // Journal Line
    public static readonly string JournalLine_AccountRequired = "Account is required.";
    public static readonly string JournalLine_DebitCannotBeNegative = "Debit amount cannot be negative.";
    public static readonly string JournalLine_CreditCannotBeNegative = "Credit amount cannot be negative.";
    public static readonly string JournalLine_BothDebitAndCredit = "A journal line cannot have both debit and credit values.";
    public static readonly string JournalLine_MustHaveDebitOrCredit = "A journal line must have either a debit or credit value.";
    public static readonly string Journal_InvalidEntryType = "The selected journal entry type is invalid or does not exist.";

    #endregion

    #region Account
    //Accounts
    public const string Account_CodeRequired = "Account code cannot be empty.";
    public const string Account_CodeMaxLength = "Account code length exceeds the maximum limit.";
    public const string Account_NameRequired = "Account name cannot be empty.";
    public const string Account_NameMaxLength = "Account name length exceeds the maximum limit.";
    public const string Account_InvalidLevel = "The selected account level is invalid.";
    public const string Account_InvalidNature = "The selected account nature is invalid.";

    public const string Account_DuplicateCode = "Account.DuplicateCode";
    public const string Account_DuplicateCodeMessage = "The requested user was not found.";
    #endregion

}