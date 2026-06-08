using System;
using BuildingBlocks.Domain.Guards.Extensions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Guards;

public class GuidGuardTests
{
    [Fact]
    public void NotEmpty_ShouldThrowArgumentException_WhenGuidIsEmpty()
    {
        Guid value = Guid.Empty;

        Assert.Throws<ArgumentException>(() => Guard.Against.NotEmpty(value));
    }

    [Fact]
    public void NotEmpty_ShouldReturnValue_WhenGuidIsNotEmpty()
    {
        Guid value = Guid.NewGuid();

        var result = Guard.Against.NotEmpty(value);

        Assert.Equal(value, result);
    }
}