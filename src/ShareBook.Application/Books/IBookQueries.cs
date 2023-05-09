using ShareBook.Application.Books.GetBooks;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;

namespace ShareBook.Application.Books;

public interface IBookQueries
{
    Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title);
}