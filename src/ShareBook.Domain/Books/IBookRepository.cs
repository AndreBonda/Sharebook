using ShareBook.Domain.Shared;

namespace ShareBook.Domain.Books;

public interface IBookRepository : IRepository<Book, Guid>
{
    Task Update(Book book);
}