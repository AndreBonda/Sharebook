namespace ShareBook.Domain.Shared.Primitives;

public interface IEventContainer 
{
    IEnumerable<DomainEvent> Events();
    void ClearEvents();
}