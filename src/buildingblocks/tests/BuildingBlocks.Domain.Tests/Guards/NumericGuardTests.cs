using System;
using BuildingBlocks.Domain.Guards.Extensions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Guards;

public class NumericGuardTests
{
    [Theory]
    [InlineData(5, 1, 10)]
    [InlineData(1, 1, 10)]
    [InlineData(10, 1, 10)]
    public void OutOfRange_ShouldReturnValue_WhenValueIsInRange(int value, int min, int max)
    {
        var result = Guard.Against.OutOfRange(value, min, max);
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(0, 1, 10)]
    [InlineData(11, 1, 10)]
    public void OutOfRange_ShouldThrowArgumentOutOfRangeException_WhenValueIsOutOfRange(int value, int min, int max)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(value, min, max));
    }

    [Fact]
    public void Positive_ShouldReturnValue_WhenValueIsPositive()
    {
        int value = 10;
        var result = Guard.Against.Positive(value);
        Assert.Equal(value, result);
    }

    [Fact]
    public void Positive_ShouldThrowArgumentOutOfRangeException_WhenValueIsNegative()
    {
        int value = -1;
        Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.Positive(value));
    }
}