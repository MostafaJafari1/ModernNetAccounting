using BuildingBlocks.Domain.Rules;

namespace Accounting.Core.Domain.Journals.Rules;

public sealed class JournalMustBeBalancedRule : IBusinessRule
{
    private readonly decimal _totalDebit;
    private readonly decimal _totalCredit;

    public JournalMustBeBalancedRule(decimal totalDebit, decimal totalCredit)
    {
        _totalDebit = totalDebit;
        _totalCredit = totalCredit;
    }

    public bool IsBroken()
        => _totalDebit != _totalCredit;

    public string Message
        => $"Journal must be balanced. Debit: {_totalDebit}, Credit: {_totalCredit}";
}