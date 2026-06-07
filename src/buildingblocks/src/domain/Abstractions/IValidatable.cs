using BuildingBlocks.Domain.Rules;

namespace BuildingBlocks.Domain.Abstractions;
public interface IValidatable
{
    void CheckRule(IBusinessRule rule);
}
