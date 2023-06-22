namespace ShareBook.Domain.Shared.Primitives;

public abstract class Entity<T>
{
    public T Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Entity(T id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    protected Entity()
    {}

    public static bool operator ==(Entity<T> left, Entity<T> right)
    {
        if (left is null && right is null)
        {
            return true;
        }
        else if (left is null ^ right is null)
        {
            return false;
        }
        else
        {
            return left.Equals(right);
        }
    }

    public static bool operator !=(Entity<T> left, Entity<T> right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = obj as Entity<T>;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}