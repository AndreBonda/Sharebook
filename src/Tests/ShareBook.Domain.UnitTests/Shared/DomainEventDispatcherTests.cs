using MediatR;
using Moq;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared;

[TestFixture]
public class DomainEventDispatcherTests
{
    [Test]
    public async Task DispatchAndClearEventsAsync_DispatchEventsAndClearEventContainers()
    {
        // Arrange
        Mock<IMediator> mediator = new();
        Mock<DomainEvent> domainEventOne = new();
        Mock<DomainEvent> domainEventTwo = new();
        DomainEventDispatcher sut = new (mediator.Object);

        // Act
        await sut.DispatchEventsAsync(new DomainEvent[]{
            domainEventOne.Object,
            domainEventTwo.Object
        });

        // Assert
        mediator.Verify(m => m.Publish(It.IsAny<DomainEvent>(), new CancellationToken()), Times.Exactly(2));
    }
}