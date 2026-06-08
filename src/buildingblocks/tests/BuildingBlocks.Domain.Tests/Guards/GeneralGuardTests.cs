using System;
using BuildingBlocks.Domain.Guards.Extensions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Guards;

public class GeneralGuardTests
{
    [Fact]
    public void Must_ShouldReturnValue_WhenPredicateIsTrue()
    {
        int value = 10;

        var result = Guard.Against.Must(value, x => x > 0, "Error");

        Assert.Equal(value, result);
    }

    [Fact]
    public void Must_ShouldThrowArgumentException_WhenPredicateIsFalse()
    {
        string value = "test";
        string errorMessage = "Invalid value";

        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.Must(value, x => x.Length > 10, errorMessage));

        Assert.StartsWith(errorMessage, exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Must_ShouldWorkWithComplexObjects()
    {
        var user = new { Id = 1, Name = "Admin" };

        var result = Guard.Against.Must(user, u => u.Name == "Admin", "User must be Admin");

        Assert.Same(user, result);
    }
}