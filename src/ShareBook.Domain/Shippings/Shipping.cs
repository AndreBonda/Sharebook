using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shippings;

public class Shipping : AggregateRoot<Guid>
{
    protected Shipping(Guid id, Guid bookId) : base(id)
    {
        BookId = bookId;
    }

    public Guid BookId { get; private set; }

    public static Shipping New(Guid id, Guid bookId)
    {
        return new Shipping(id, bookId);
    }
}