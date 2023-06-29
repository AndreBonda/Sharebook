using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Infrastructure;

namespace ShareBook.Application.IntegrationTest.Books;

[TestFixture]
public class BookQueriesTests
{
    public readonly DbContextOptions<AppDbContext> _dbContextOptions;
    public AppDbContext _ctx;

    public BookQueriesTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ShareBookTest")
            .Options;

        _ctx = new AppDbContext(_dbContextOptions);
    }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        // await _ctx.Books.AddRangeAsync(new List<Book>()
        // {
        //             Book.New(Guid.NewGuid(),"OwnerX", "TitleX", "AuthorX", 2, true, new string[] {"LabelX"}),
        //             Book.New(Guid.NewGuid(), "OwnerY", "TitleY", "AuthorY", 2, true, new string[] {"LabelY"}),
        //             Book.New(Guid.NewGuid(), "OwnerZ", "TitleZ", "AuthorZ", 2, true, new string[] {"LabelZ"})
        // });
        // await _ctx.SaveChangesAsync();
    }
}