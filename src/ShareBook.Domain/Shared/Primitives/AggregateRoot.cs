namespace ShareBook.Domain.Shared.Primitives;

public abstract class AggregateRoot<T> : Entity<T>, IEventContainer
{
    private readonly List<DomainEvent> _events = new();

    protected AggregateRoot(T id) : base(id)
    { }

    public IEnumerable<DomainEvent> Events() => _events;
    public void ClearEvents() => _events.Clear();
    protected void RegisterEvent(DomainEvent @event) => _events.Add(@event);
}
