using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;

namespace ShareBook.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book, Guid>, IBookRepository
{
    public BookRepository(AppDbContext ctx) : base(ctx)
    {
    }

    public async override Task AddAsync(Book entity)
    {
        await _ctx.Books.AddAsync(entity);
    }

    public override async Task<Book?> GetByIdAsync(Guid id) =>
        await _ctx.Books.FirstOrDefaultAsync(b => b.Id == id);
}