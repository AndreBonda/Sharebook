using FluentAssertions;
using NSubstitute;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class AggregateRootTests
{
    private class FakeAggregateRoot : AggregateRoot<Guid>
    {
        public FakeAggregateRoot(Guid id) : base(id)
        {
        }

        public void FakeRaise(DomainEvent @event) {
            RaiseEvent(@event);
        }

        public IEnumerable<DomainEvent> FakeRealeaseEvents() {
            return ReleaseEvents();
        }
    }

    [Test]
    public void RegisterEvent_ReleaseEvents_WhenEventIsPassed()
    {
        // Arrange
        FakeAggregateRoot sut = new FakeAggregateRoot(Guid.NewGuid());

        // Act
        sut.FakeRaise(Substitute.For<DomainEvent>());
        sut.FakeRaise(Substitute.For<DomainEvent>());
        IEnumerable<DomainEvent> releasedEvents = sut.ReleaseEvents();
        IEnumerable<DomainEvent> emptyReleasedEvents = sut.ReleaseEvents();

        // Assert
        releasedEvents.Count().Should().Be(2);
        emptyReleasedEvents.Count().Should().Be(0);
    }
}