using MediatR;
using NSubstitute;
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
        IMediator mediator = Substitute.For<IMediator>();
        DomainEvent domainEventOne = Substitute.For<DomainEvent>();
        DomainEvent domainEventTwo = Substitute.For<DomainEvent>();
        DomainEventDispatcher sut = new (mediator);

        // Act
        await sut.DispatchEventsAsync(new [] {
            domainEventOne,
            domainEventTwo
        });

        // Assert
        await mediator.Received(2).Publish(Arg.Any<DomainEvent>());
    }
}