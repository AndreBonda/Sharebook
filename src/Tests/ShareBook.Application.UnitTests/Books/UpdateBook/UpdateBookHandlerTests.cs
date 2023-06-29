using Moq;
using ShareBook.Application.Books;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.UnitTests.Books.UpdateBook;

[TestFixture]
public class UpdateBookHandlerTests
{
    private Mock<IBookRepository> _repo;

    [SetUp]
    public void SetUp() {
        _repo = new Mock<IBookRepository>();
    }

    [Test]
    public async Task Handle_UpdateBook()
    {
        var bookId = Guid.NewGuid();
        var bookReturnedFromRepository = new Mock<Book>(
            bookId,
            "owner",
            "title",
            "author",
            2,
            true,
            new string[] {"label"}
        );

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => bookReturnedFromRepository.Object);

        var command = new UpdateBookCmd(
            bookId,
            "owner",
            "title_update",
            "author_update",
            4,
            false,
            new string[] { "label_update" }
        );

        var handler = new UpdateBookHandler(_repo.Object);

        await handler.Handle(command, new CancellationToken());

        _repo.Verify(r => r.GetByIdAsync(bookId));
        bookReturnedFromRepository.Verify(b => b.Update(
            Guid.Empty, //TODO:update
            "title_update", 
            "author_update", 
            4, 
            false, 
            new string[] { "label_update" }));
        _repo.Verify(r => r.SaveAsync());
    }

    [Test]
    public void Handle_ThrowsNotFoundException_IfBookNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var handler = new UpdateBookHandler(_repo.Object);
        var command = new UpdateBookCmd(
            Guid.NewGuid(),
            "owner",
            "title_update",
            "author_update",
            4,
            false,
            new string[] { "label_update" }
        );

        Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
    }
}