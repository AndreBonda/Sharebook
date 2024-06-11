using ShareBook.Domain.Books;
using ShareBook.Domain.Books.Events;
using ShareBook.Domain.Books.Exceptions;
using static ShareBook.Domain.Books.LoanRequest;

namespace ShareBook.UnitTests.Books;

[TestFixture]
public class BookTests
{
    [Test]
    public void New_ThrowsException_IfGuidIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => Book.New(Guid.Empty, Guid.NewGuid(), "valid_title", "valid_author", 1, true));
    }

    public void New_ThrowsArgumentNullException_IfOwnerIdIsAnEmptyGuid()
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(), Guid.Empty, "title", "author", 1, true));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void New_ThrowsArgumentNullException_IfTitleIsNullOrEmptyOrWhiteSpaces(string invalidTitle)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(), Guid.NewGuid(), invalidTitle, "author", 1, true));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void New_ThrowsArgumentNullException_IfAuthorIsNullOrEmptyOrWhiteSpaces(string invalidAuthor)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(), Guid.NewGuid(), "title", invalidAuthor, 1, true));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void New_ThrowsArgumentOutOfRangeException_IfPagesAreLessThanOne(int invalidPages)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Book.New(Guid.NewGuid(), Guid.NewGuid(), "title", "author", invalidPages, true));
    }

    [Test]
    public void New_CreateNewBook_IfValidInputs()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();

        // Act
        var book = Book.New(
            bookId,
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1", "label2" });

        // Assert
        Assert.That(book, Is.Not.Null);
        Assert.That(book.Id, Is.EqualTo(bookId));
        Assert.That(book.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(1).Minutes);
        Assert.That(book.OwnerId, Is.EqualTo(ownerId));
        Assert.That(book.Title, Is.EqualTo("title"));
        Assert.That(book.Author, Is.EqualTo("author"));
        Assert.That(book.Pages, Is.EqualTo(1));
        Assert.IsTrue(book.SharedByOwner);
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1", "label2" }));
        Assert.That(book.RequestStatus(), Is.Null);
    }

    [Test]
    public void New_CreateNewBook_IgnoringDuplicateLabels()
    {
        // Act
        var book = Book.New(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "valid_title",
            "valid_author",
            1,
            true,
            new string[] { "label1", "label1" });

        // Assert
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }

    [Test]
    public void Update_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
    {
        // Assert
        var ownerId = Guid.NewGuid();
        var notOwnerUserId = Guid.NewGuid();

        // ACT
        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

        Assert.Throws<UserIsNotBookOwnerException>(() => book.Update(
            notOwnerUserId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" }
        ));
    }

    [Test]
    public void Update_UpdateFields_IfValidInputs()
    {
        // Assert
        var ownerId = Guid.NewGuid();

        // Act
        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

        book.Update(
           ownerId,
           "title_update",
           "author_update",
           2,
           false,
           new string[] { "label2" });

        Assert.That(book.Title, Is.EqualTo("title_update"));
        Assert.That(book.Author, Is.EqualTo("author_update"));
        Assert.That(book.Pages, Is.EqualTo(2));
        Assert.IsFalse(book.SharedByOwner);
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label2" }));
    }

    [Test]
    public void Update_ThrowsRemoveSharingWithCurrentLoanRequestException_IfShareByOnwerIsSetToFalseAndCurrentLoanRequestExists()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        bool sharedByOwner = false;

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

        book.RequestNewLoan(Guid.NewGuid());

        // Act & Assert
        Assert.Throws<RemoveSharingWithCurrentLoanRequestException>(() =>
            book.Update(ownerId, "title", "author", 50, sharedByOwner, new string[] { "label1" }));
    }

    [Test]
    public void RequestNewLoan_ThrowsBookNotSharedByOwnerException_IfBookNotSharedByOwner()
    {
        // Arrange
        bool sharedByOwner = false;

        var book = Book.New(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "title",
            "author",
            1,
            sharedByOwner,
            new string[] { "label" }
            );

        // Act & Assert
        Assert.Throws<BookNotSharedByOwnerException>(() => book.RequestNewLoan(Guid.NewGuid()));
    }

    [Test]
    public void RequestNewLoan_ThrowsBookOwnerCannotMakeALoanRequest_IfOwnerMakeARequestForHisBook()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        // Act & Assert
        Assert.Throws<BookOwnerCannotMakeALoanRequest>(() => book.RequestNewLoan(ownerId));
    }

    [Test]
    public void RequestNewLoan_SetCurrentLoanRequestStatus_IfLoanRequestIsCreated()
    {
        // Arrange
        var book = Book.New(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        // Act
        book.RequestNewLoan(Guid.NewGuid());

        // Assert
        Assert.That(book.RequestStatus(), Is.EqualTo(LoanRequestStatus.WAITING_FOR_ACCEPTANCE));
    }

    [Test]
    public void RefuseLoanRequest_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var notOwnerUserId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );
        
        // Act & Assert
        Assert.Throws<UserIsNotBookOwnerException>(() => book.RefuseLoanRequest(notOwnerUserId));
    }

    [Test]
    public void RefuseLoanRequest_ThrowsNonExistingLoanRequestException_IfLoanRequestDoesNotExist()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );
        
        // Act & Assert
        Assert.Throws<NonExistingLoanRequestException>(() => book.RefuseLoanRequest(ownerId));
    }

    [Test]
    public void RefuseLoanRequest_ThrowsLoanRequestAlreadyAcceptedException_IfLoanRequestIdAlreadyAccepted()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        book.RequestNewLoan(requestingUserId);
        book.AcceptLoanRequest(ownerId);

        // Act & Assert
        Assert.Throws<LoanRequestAlreadyAcceptedException>(() => book.AcceptLoanRequest(ownerId));
    }

    [Test]
    public void RefuseLoanRequest_SetCurrentLoanRequestStatusToNull_IfLoanRequestIsRefused()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        book.RequestNewLoan(requestingUserId);
        book.RefuseLoanRequest(ownerId);

        // Act & Assert
        Assert.That(book.RequestStatus(), Is.Null);
    }

    [Test]
    public void AcceptLoanRequest_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var notOwnerUserId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );
        
        // Act & Assert
        Assert.Throws<UserIsNotBookOwnerException>(() => book.AcceptLoanRequest(notOwnerUserId));
    }

    [Test]
    public void AcceptLoanRequest_ThrowsNonExistingLoanRequestException_IfLoanRequestDoesNotExist()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );
        
        // Act & Assert
        Assert.Throws<NonExistingLoanRequestException>(() => book.AcceptLoanRequest(ownerId));
    }

    [Test]
    public void AcceptLoanRequest_UpdatesBook_IfLoanRequestIsAccepted()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var book = Book.New(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        book.RequestNewLoan(requestingUserId);

        // Act
        book.AcceptLoanRequest(ownerId);

        // Assert
        var events = book.ReleaseEvents();

        Assert.That(book.RequestStatus(), Is.EqualTo(LoanRequestStatus.ACCEPTED));
        Assert.That(events, Has.Exactly(1).TypeOf<LoanRequestAcceptedEvent>());

        var @event = ((LoanRequestAcceptedEvent)events.First());

        Assert.That(@event.BookId, Is.EqualTo(book.Id));
        Assert.That(@event.DateOcurred, Is.EqualTo(DateTime.UtcNow).Within(1).Minutes);
    }
}