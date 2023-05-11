using Moq;
using ShareBook.Domain.Books;

namespace ShareBook.UnitTests.Books;

[TestFixture]
public class AcceptLoanRequestServiceTests 
{
    private Guid _bookId;
    private Mock<Book> _book;
    private AcceptLoanRequestService _service;

    [SetUp]
    public void SetUp() {
        _bookId = Guid.NewGuid();
        _book = new Mock<Book>(
            _bookId,
            "owner",
            "title",
            "author",
            2,
            true,
            new string[] { "label" }
        );
        _service = new AcceptLoanRequestService();
    }

    [Test]
    public void AcceptLoanRequest_ThrowsArgumentNullException_IfBookIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _service.AcceptLoanRequest(null, "book_owner"));
    }

    [Test]
    public void AcceptLoanRequest_UpdateBookStatusAndReturnsNewShipping_IfLoanRequestIsAccepted()
    {
        _book.Setup(b => b.AcceptLoanRequest(It.IsAny<string>()));
        var newShipping = _service.AcceptLoanRequest(_book.Object, "owner");

        _book.Verify(b => b.AcceptLoanRequest("owner"));
        Assert.That(newShipping, Is.Not.Null);
        Assert.That(newShipping.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(newShipping.BookId, Is.EqualTo(_bookId));
    }
}