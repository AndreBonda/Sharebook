using Moq;
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
        Mock<DomainEvent> domainEventOne = new();
        Mock<DomainEvent> domainEventTwo = new();
        FakeAggregateRoot sut = new FakeAggregateRoot(Guid.NewGuid());

        // Act
        sut.FakeRaise(domainEventOne.Object);
        sut.FakeRaise(domainEventTwo.Object);
        IEnumerable<DomainEvent> releasedEvents = sut.ReleaseEvents();
        IEnumerable<DomainEvent> emptyReleasedEvents = sut.ReleaseEvents();

        // Assert
        Assert.That(releasedEvents.Count(), Is.EqualTo(2));
        Assert.That(emptyReleasedEvents.Count(), Is.EqualTo(0));
    }
}