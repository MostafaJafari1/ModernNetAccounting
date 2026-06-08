using System;
using BuildingBlocks.Domain.Guards.Extensions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Guards;

public class ObjectGuardTests
{
    [Fact]
    public void NotNull_ShouldThrowArgumentNullException_WhenValueIsNull()
    {
        string? value = null;

        Assert.Throws<ArgumentNullException>(() => Guard.Against.NotNull(value!));
    }

    [Fact]
    public void NotNull_ShouldReturnValue_WhenValueIsNotNull()
    {
        var value = new object();

        var result = Guard.Against.NotNull(value);

        Assert.Same(value, result);
    }
}