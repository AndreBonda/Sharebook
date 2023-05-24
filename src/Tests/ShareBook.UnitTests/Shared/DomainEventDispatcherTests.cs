using MediatR;
using Moq;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared;

[TestFixture]
public class DomainEventDispatcherTests
{

    private Mock<IMediator> _mediator;
    private Mock<IEventContainer> _eventContainerOne;
    private Mock<DomainEvent> _domainEventOne;
    private Mock<IEventContainer> _eventContainerTwo;
    private Mock<DomainEvent> _domainEventTwo;
    private DomainEventDispatcher _eventDispatcher;

    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<IMediator>();

        _eventContainerOne = new Mock<IEventContainer>();
        _domainEventOne = new Mock<DomainEvent>();

        _eventContainerTwo = new Mock<IEventContainer>();
        _domainEventTwo = new Mock<DomainEvent>();
    }

    [Test]
    public async Task DispatchAndClearEventsAsync_DispatchEventsAndClearEventContainers()
    {
        _eventContainerOne
            .Setup(ec => ec.Events())
            .Returns(new List<DomainEvent>(){_domainEventOne.Object});

        _eventContainerTwo
            .Setup(ec => ec.Events())
            .Returns(new List<DomainEvent>() { _domainEventTwo.Object });

        _eventDispatcher = new(_mediator.Object);

        List<IEventContainer> eventContainers = new() {
            _eventContainerOne.Object,
            _eventContainerTwo.Object
        };

        await _eventDispatcher.DispatchAndClearEventsAsync(eventContainers);

        _eventContainerOne.Verify(ec => ec.Events(), Times.Once);
        _eventContainerOne.Verify(ec => ec.ClearEvents(), Times.Once);
        _eventContainerTwo.Verify(ec => ec.Events(), Times.Once);
        _eventContainerTwo.Verify(ec => ec.ClearEvents(), Times.Once);
        _mediator.Verify(m => m.Publish(It.IsAny<DomainEvent>(), new CancellationToken()), Times.Exactly(2));
    }
}