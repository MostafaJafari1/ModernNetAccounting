using BuildingBlocks.Domain.Rules;

namespace BuildingBlocks.Domain.Exceptions;

public class BusinessRuleValidationException : DomainException
{
    public IBusinessRule? BrokenRule { get; init; }
    public string Details { get; init; }

    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.Message;
    }

    public BusinessRuleValidationException(string message)
        : base(message)
    {
        Details = message;
    }

    public override string ToString()
    {
        return BrokenRule != null
            ? $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}"
            : $"{base.GetType().Name}: {Message}";
    }
}