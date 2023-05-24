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

        public void FakeRegister(DomainEvent @event) {
            RegisterEvent(@event);
        }
    }

    private Mock<DomainEvent> _domainEvent;
    private FakeAggregateRoot _aggregateRoot;

    [SetUp]
    public void SetUp() {
        _domainEvent = new Mock<DomainEvent>();
        _aggregateRoot = new FakeAggregateRoot(Guid.NewGuid());
    }

    [Test]
    public void RegisterEvent_AddsEvent_WhenEventIsPassed()
    {
        _aggregateRoot.FakeRegister(_domainEvent.Object);

        Assert.That(_aggregateRoot.Events().Count(), Is.EqualTo(1));
    }

    [Test]
    public void ClearEvents_ClearsAllExistingEvents()
    {
        _aggregateRoot.FakeRegister(_domainEvent.Object);

        _aggregateRoot.ClearEvents();

        Assert.That(_aggregateRoot.Events().Count(), Is.EqualTo(0));
    }
}