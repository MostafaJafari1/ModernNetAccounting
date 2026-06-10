using BuildingBlocks.Common.Guards;
using System;
using System.Collections.Generic;
using Xunit;
using BuildingBlocks.Common.Guards.Extensions;

namespace BuildingBlocks.Domain.Tests.Guards;

public class CollectionGuardTests
{
    [Fact]
    public void NotEmpty_ShouldThrowArgumentException_WhenCollectionIsNull()
    {
        IEnumerable<string>? myCollection = null;

        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.NotEmpty(myCollection!));

        Assert.Contains("myCollection", exception.ParamName);
    }

    [Fact]
    public void NotEmpty_ShouldThrowArgumentException_WhenCollectionIsEmpty()
    {
        var myCollection = new List<int>();

        Assert.Throws<ArgumentException>(() =>
            Guard.Against.NotEmpty(myCollection));
    }

    [Fact]
    public void NotEmpty_ShouldReturnCollection_WhenCollectionHasElements()
    {
        var myCollection = new List<string> { "Test1", "Test2" };

        var result = Guard.Against.NotEmpty(myCollection);

        Assert.Same(myCollection, result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void NotEmpty_ShouldThrowExceptionWithCustomMessage_WhenProvided()
    {
        var myCollection = new List<int>();
        var customMessage = "List must not be null";

        var exception = Assert.Throws<ArgumentException>(() =>
            Guard.Against.NotEmpty(myCollection, customMessage));

        Assert.StartsWith(customMessage, exception.Message);
        Assert.Contains("myCollection", exception.Message);
    }
}