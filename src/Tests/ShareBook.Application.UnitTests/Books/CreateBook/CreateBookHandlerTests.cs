using Moq;
using ShareBook.Application.Books.CreateBook;
using ShareBook.Domain.Books;

namespace ShareBook.Application.UnitTests.Books.CreateBook;

[TestFixture]
public class CreateBookHandlerTests
{
    private Mock<IBookRepository> _repo;

    [SetUp]
    public void SetUp() {
        _repo = new Mock<IBookRepository>();
    }

    [Test]
    public async Task Handle_CreateNewBook()
    {
        var id = Guid.NewGuid();
        var command = new CreateBookCmd(
            id,
            "owner",
            "title",
            "author",
            1,
            true,
            new string[] { "label" });

        var handler = new CreateBookHandler(_repo.Object);

        await handler.Handle(command, new CancellationToken());

        _repo.Verify(x => x.AddAsync(It.Is<Book>(b => 
            b.Id == id &&
            b.Owner == "owner" &&
            b.Title == "title" &&
            b.Author == "author" &&
            b.Pages == 1 &&
            b.SharedByOwner == true &&
            b.Labels.Count() == 1 &&
            b.Labels.First() == "label")));
        _repo.Verify(x => x.SaveAsync());
    }
}