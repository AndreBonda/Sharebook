using ShareBook.Application.Books.GetBooks;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;

namespace ShareBook.Application.Books;

public class BookQueries
{
    private readonly IAppDbContext _ctx;

    public BookQueries(IAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title)
    {
        var query = _ctx.Books.AsQueryable();

        if(!string.IsNullOrWhiteSpace(title))
            query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));

        return (
            await query.ToListAsync())
            .Select(b => new BookVM(b.Id, b.Owner, b.Title, b.Author, b.Pages, b.SharedByOwner, b.Labels));
    }
}