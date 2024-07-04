using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;
using ShareBook.Infrastructure.DataModel;

namespace ShareBook.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book, Guid>, IBookRepository
{
    public BookRepository(AppDbContext ctx) : base(ctx) { }

    public override async Task AddAsync(Book entity)
    {
        await _ctx.Books.AddAsync(new BookData
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            Title = entity.Title,
            Author = entity.Author,
            Pages = entity.Pages,
            SharedByOwner = entity.SharedByOwner,
            Labels = entity.Labels
        });
        await _ctx.SaveChangesAsync();
    }

    public async Task Update(Book book)
    {
        BookData? bookData = await _ctx.Books
            .Include(b => b.LoanRequests)
            .FirstOrDefaultAsync(b => b.Id == book.Id);

        if (bookData is null) return;

        bookData.OwnerId = book.OwnerId;
        bookData.Title = book.Title;
        bookData.Author = book.Author;
        bookData.Pages = book.Pages;
        bookData.SharedByOwner = book.SharedByOwner;
        bookData.Labels = book.Labels;
        bookData.LoanRequests = book.CurrentLoanRequests.Select(lr => new LoanRequestData()
        {
            Id = lr.Id,
            BookId = book.Id,
            UserId = lr.RequestingUserId,
            Status = lr.Status
        })
        .ToList();

        await _ctx.SaveChangesAsync();
    }

    public override async Task<Book?> GetByIdAsync(Guid id)
    {
        BookData? bookData = await _ctx.Books
            .Include(bookData => bookData.LoanRequests)
            .AsNoTracking()
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