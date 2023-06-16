using MediatR;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shared;

public class DomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventsAsync(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
            await _mediator.Publish(@event);
    }
}