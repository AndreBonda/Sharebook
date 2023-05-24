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

    public async Task DispatchAndClearEventsAsync(IEnumerable<IEventContainer> eventContainers)
    {
        IEnumerable<DomainEvent> events = eventContainers.SelectMany(ec => ec.Events()).ToArray();

        foreach (var ec in eventContainers)
            ec.ClearEvents();

        foreach (var @event in events)
            await _mediator.Publish(@event);
    }
}