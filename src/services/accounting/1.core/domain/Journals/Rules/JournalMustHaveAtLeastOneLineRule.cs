using BuildingBlocks.Domain.Rules;

namespace Accounting.Core.Domain.Journals.Rules;

public sealed class JournalMustHaveAtLeastOneLineRule : IBusinessRule
{
    private readonly int _lineCount;

    public JournalMustHaveAtLeastOneLineRule(int lineCount)
    {
        _lineCount = lineCount;
    }

    public bool IsBroken()
        => _lineCount == 0;

    public string Message
        => "Journal must have at least one line";
}