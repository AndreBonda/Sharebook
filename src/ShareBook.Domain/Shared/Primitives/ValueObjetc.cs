namespace ShareBook.Domain.Shared.Primitives;

public abstract class ValueObject
{
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if(left is null && right is null)
        {
            return true;
        }

        if (left is null ^ right is null)
        {
            return false;
        }

        return left!.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (obj as ValueObject)!;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();
}