using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public interface IBookQueries
{
    Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title);
    Task<BookVM?> GetBookByIdAsync(Guid id);
}