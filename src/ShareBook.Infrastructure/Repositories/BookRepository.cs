using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;
using ShareBook.Infrastructure.DataModel;

namespace ShareBook.Infrastructure.Repositories;

public class BookRepository(AppDbContext ctx) : BaseRepository<Book, Guid>(ctx), IBookRepository
{
    public override async Task AddAsync(Book entity)
    {
        await ctx.Books.AddAsync(new BookData
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            Title = entity.Title,
            Author = entity.Author,
            Pages = entity.Pages,
            SharedByOwner = entity.SharedByOwner,
            Labels = entity.Labels
        });
    }

    public override async Task<Book?> GetByIdAsync(Guid id)
    {
        BookData? bookData = await ctx.Books
            .Include(bookData => bookData.LoanRequests)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (bookData is null) return null;

        return new Book(
            bookData.Id,
            bookData.OwnerId,
            bookData.Title,
            bookData.Author,
            bookData.Pages,
            bookData.SharedByOwner,
            bookData.Labels,
            bookData.LoanRequests.Select(lr => new LoanRequest(lr.Id, lr.UserId, lr.Status)));
    }
}