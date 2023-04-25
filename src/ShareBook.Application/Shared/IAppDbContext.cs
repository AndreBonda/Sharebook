using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Shared;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }
}