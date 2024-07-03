using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Books.ViewModels;
using ShareBook.Infrastructure.DataModel;

namespace ShareBook.Infrastructure.Queries;

public class BookQueries(AppDbContext ctx) : IBookQueries
{
    public async Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string? title, int offset, int limit)
    {
        var query = ctx.Books
            .Include(b => b.Owner)
            .Include(b => b.LoanRequests)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));
        }

        List<BookData> books = await query.Skip(offset).Take(limit).ToListAsync();

        return books.Select(book => new BookVM()
        {
            Id = book.Id,
            OwnerEmail = book.Owner.Email,
            Title = book.Title,
            Author = book.Author,
            Pages = book.Pages,
            Labels = book.Labels,
            SharedByOwner = book.SharedByOwner,
            LoanRequests = book.LoanRequests.Select(lr => new LoanRequestVM()
            {
                Id = lr.Id,
                RequestingUserId = lr.UserId,
                Status = lr.Status
            }).ToArray()
        });
    }

    public async Task<BookVM?> GetBookByIdAsync(Guid id)
    {
        BookData? book = await ctx.Books
            .Include(b => b.Owner)
            .Include(b => b.LoanRequests)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null) return null;

        return new BookVM()
        {
            Id = book.Id,
            OwnerEmail = book.Owner.Email,
            Title = book.Title,
            Author = book.Author,
            Pages = book.Pages,
            Labels = book.Labels,
            SharedByOwner = book.SharedByOwner,
            LoanRequests = book.LoanRequests.Select(lr => new LoanRequestVM()
            {
                Id = lr.Id,
                RequestingUserId = lr.UserId,
                Status = lr.Status
            }).ToArray()
        };
    }
}