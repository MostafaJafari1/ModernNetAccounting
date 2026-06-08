using BuildingBlocks.Domain.Primitives;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Tests.Primitives;

public class EnumerationTests
{
    private class TestEnum : Enumeration<TestEnum>
    {
        public static readonly TestEnum First = new(1, "First");
        public static readonly TestEnum Second = new(2, "Second");

        public TestEnum(int value, string name) : base(value, name) { }
    }

    [Fact]
    public void GetValues_ReturnsAllDefinedEnumerations()
    {
        var values = TestEnum.GetValues();

        values.Should().HaveCount(2);
        values.Should().Contain(TestEnum.First);
        values.Should().Contain(TestEnum.Second);
    }

    [Fact]
    public void FromValue_WithValidValue_ReturnsCorrectEnumeration()
    {
        var result = TestEnum.FromValue(1);

        result.Should().Be(TestEnum.First);
    }

    [Fact]
    public void FromValue_WithInvalidValue_ReturnsNull()
    {
        var result = TestEnum.FromValue(99);

        result.Should().BeNull();
    }

    [Fact]
    public void FromName_WithValidName_ReturnsCorrectEnumeration()
    {
        var result = TestEnum.FromName("Second");

        result.Should().Be(TestEnum.Second);
    }

    [Fact]
    public void FromName_WithInvalidName_ReturnsNull()
    {
        var result = TestEnum.FromName("Invalid");

        result.Should().BeNull();
    }

    [Fact]
    public void Equals_SameInstances_ReturnsTrue()
    {
        var enum1 = TestEnum.First;
        var enum2 = TestEnum.First;

        enum1.Equals(enum2).Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        var enum1 = TestEnum.First;
        var enum2 = TestEnum.Second;

        enum1.Equals(enum2).Should().BeFalse();
    }

    [Fact]
    public void ToString_ReturnsName()
    {
        TestEnum.First.ToString().Should().Be("First");
    }

    [Fact]
    public void GetHashCode_SameValue_ReturnsSameHashCode()
    {
        TestEnum.First.GetHashCode().Should().Be(TestEnum.First.GetHashCode());
    }
}
