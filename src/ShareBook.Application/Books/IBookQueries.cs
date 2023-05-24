namespace ShareBook.Application.Books;

public interface IBookQueries
{
    Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title);
}