using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.Rules;
using Moq;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Rules;

public class CheckTests
{
    [Fact]
    public void Rule_ShouldNotThrowException_WhenRuleIsNotBroken()
    {
        var ruleMock = new Mock<IBusinessRule>();
        ruleMock.Setup(x => x.IsBroken()).Returns(false);

        Check.Rule(ruleMock.Object);
    }

    [Fact]
    public void Rule_ShouldThrowBusinessRuleValidationException_WhenRuleIsBroken()
    {
        var ruleMock = new Mock<IBusinessRule>();
        ruleMock.Setup(x => x.IsBroken()).Returns(true);

        Assert.Throws<BusinessRuleValidationException>(() => Check.Rule(ruleMock.Object));
    }
}