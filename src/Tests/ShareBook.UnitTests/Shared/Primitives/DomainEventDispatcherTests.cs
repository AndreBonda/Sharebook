using MediatR;
using Moq;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class DomainEventDispatcherTests
{
    private Mock<IMediator> _mediator;
    private Mock<IEventContainer> _ecOne;
    private Mock<IEventContainer> _ecTwo;

    [SetUp]
    public void SetUp() {
        _mediator = new Mock<IMediator>();
        _ecOne = new Mock<IEventContainer>();
        _ecTwo = new Mock<IEventContainer>();
    }

    [Test]
    public void DispatchAndClearEventsAsync_ItDispatchAllEventsAndClearEventContainers()
    {
        
    }

}