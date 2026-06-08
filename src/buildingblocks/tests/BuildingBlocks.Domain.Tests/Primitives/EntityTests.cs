using BuildingBlocks.Domain.Primitives;
using FluentAssertions;
using Xunit;

namespace BuildingBlocks.Domain.Tests.Primitives;

public class EntityTests
{
    private class TestEntity : Entity
    {
        public TestEntity(Guid id) : base(id) { }
    }

    [Fact]
    public void Constructor_WithValidId_SetsIdCorrectly()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntity(id);

        entity.Id.Should().Be(id);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ThrowsArgumentException()
    {
        var action = () => new TestEntity(Guid.Empty);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Equals_SameId_ReturnsTrue()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        entity1.Equals(entity2).Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentId_ReturnsFalse()
    {
        var entity1 = new TestEntity(Guid.NewGuid());
        var entity2 = new TestEntity(Guid.NewGuid());

        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void OperatorEquality_SameId_ReturnsTrue()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        (entity1 == entity2).Should().BeTrue();
    }

    [Fact]
    public void OperatorInequality_DifferentId_ReturnsTrue()
    {
        var entity1 = new TestEntity(Guid.NewGuid());
        var entity2 = new TestEntity(Guid.NewGuid());

        (entity1 != entity2).Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_SameId_ReturnsSameHashCode()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }
}
