namespace ShareBook.Domain.Shared.Primitives;

public abstract class AggregateRoot<T> : Entity<T>
{
    private readonly List<DomainEvent> _events = new();

    protected AggregateRoot(T id) : base(id)
    { }

    public IEnumerable<DomainEvent> ReleaseEvents()
    {
        List<DomainEvent> events = _events.ToList();
        _events.Clear();
        return events;
    }

    protected void RaiseEvent(DomainEvent @event) => _events.Add(@event);
}
