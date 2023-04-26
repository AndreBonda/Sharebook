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
        await _ctx.Books.AddRangeAsync(new List<Book>()
        {
                    Book.New(Guid.NewGuid(),"OwnerX", "TitleX", "AuthorX", 2, new string[] {"LabelX"}),
                    Book.New(Guid.NewGuid(), "OwnerY", "TitleY", "AuthorY", 2, new string[] {"LabelY"}),
                    Book.New(Guid.NewGuid(), "OwnerZ", "TitleZ", "AuthorZ", 2, new string[] {"LabelZ"})
        });
        await _ctx.SaveChangesAsync();
    }

    [TestCase("")]
    [TestCase(null)]
    public async Task GetBooksByTitleAsync_ReturnsAllBooks_IfNoTitlePassed(string emptyTitle)
    {
        var bookQueries = new BookQueries(_ctx);

        var books = await bookQueries.GetBooksByTitleAsync(emptyTitle);

        Assert.That(books.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetBooksByTitleAsync_ReturnsEmptyCollection_IfNoBooksFound()
    {
        var bookQueries = new BookQueries(_ctx);

        var books = await bookQueries.GetBooksByTitleAsync("no_existing_book_title");

        Assert.That(books.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetBooksByTitleAsync_ReturnsExpectedBooks_IfTitleParamPassed()
    {
        var bookQueries = new BookQueries(_ctx);

        var books = await bookQueries.GetBooksByTitleAsync("y");

        Assert.That(books.Count(), Is.EqualTo(1));
        Assert.That(books.First().Title, Is.EqualTo("TitleY"));
    }

}