using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.Rules;
public static class Check
{
    public static void Rule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}
