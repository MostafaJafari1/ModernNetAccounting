using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Primitives;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Tests.Primitives;
public class AggregateRootTests
{
    private class TestAggregateRoot : AggregateRoot
    {
        public TestAggregateRoot(Guid id) : base(id) { }
        public void AddEvent(IDomainEvent domainEvent) => RaiseDomainEvent(domainEvent);
    }

    private class TestDomainEvent : IDomainEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime HappenedAt => DateTime.UtcNow;
    }

    [Fact]
    public void GetDomainEvents_Initially_ReturnsEmptyCollection()
    {
        var aggregate = new TestAggregateRoot(Guid.NewGuid());

        aggregate.GetDomainEvents().Should().BeEmpty();
    }

    [Fact]
    public void RaiseDomainEvent_AddsEventToCollection()
    {
        var aggregate = new TestAggregateRoot(Guid.NewGuid());
        var domainEvent = new TestDomainEvent();

        aggregate.AddEvent(domainEvent);

        aggregate.GetDomainEvents().Should().ContainSingle().Which.Should().Be(domainEvent);
    }

    [Fact]
    public void ClearDomainEvents_RemovesAllEvents()
    {
        var aggregate = new TestAggregateRoot(Guid.NewGuid());
        aggregate.AddEvent(new TestDomainEvent());

        aggregate.ClearDomainEvents();

        aggregate.GetDomainEvents().Should().BeEmpty();
    }

    [Fact]
    public void GetDomainEvents_ReturnsReadOnlyCollection()
    {
        var aggregate = new TestAggregateRoot(Guid.NewGuid());
        var events = aggregate.GetDomainEvents();

        var canCastToList = events is List<IDomainEvent>;

        canCastToList.Should().BeFalse();
    }
}