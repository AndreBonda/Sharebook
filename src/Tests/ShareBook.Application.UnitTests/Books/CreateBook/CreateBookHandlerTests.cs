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
        var command = new CreateBookCmd(
            "owner",
            "title",
            "author",
            1,
            new string[] { "label" });

        var handler = new CreateBookHandler(_repo.Object);

        var guid = await handler.Handle(command, new CancellationToken());

        _repo.Verify(x => x.AddAsync(It.Is<Book>(b => 
            b.Owner == "owner" &&
            b.Title == "title" &&
            b.Author == "author" &&
            b.Pages == 1 &&
            b.Labels.Count() == 1 &&
            b.Labels.First() == "label")));
        _repo.Verify(x => x.SaveAsync());
        Assert.That(guid, Is.Not.EqualTo(Guid.Empty));
    }


}