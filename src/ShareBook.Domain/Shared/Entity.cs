namespace ShareBook.Domain.Shared;

public abstract class Entity<T> {
    public T Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Entity(T id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}