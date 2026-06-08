using System;
using BuildingBlocks.Domain.Guards.Extensions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Guards;

public class StringGuardTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void NullOrEmpty_ShouldThrowArgumentException_WhenValueIsInvalid(string? value)
    {
        Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(value!));
    }

    [Fact]
    public void NullOrEmpty_ShouldReturnValue_WhenValueIsValid()
    {
        string value = "valid_string";
        var result = Guard.Against.NullOrEmpty(value);
        Assert.Equal(value, result);
    }

    [Fact]
    public void MaxLength_ShouldThrowArgumentException_WhenValueExceedsLimit()
    {
        string value = "Hello World";
        int limit = 5;
        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.MaxLength(value, limit));

        Assert.Contains("longer than 5 characters", exception.Message);
    }

    [Theory]
    [InlineData("Test", 4)]
    [InlineData("Test", 10)]
    public void MaxLength_ShouldReturnValue_WhenValueIsWithinLimit(string value, int limit)
    {
        var result = Guard.Against.MaxLength(value, limit);
        Assert.Equal(value, result);
    }
}