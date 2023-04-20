using ShareBook.Application.Books.GetBooks;

namespace ShareBook.API.Books;

public interface IBookQueries
{
    Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string name);
}