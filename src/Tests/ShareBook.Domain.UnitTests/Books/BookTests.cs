using FluentAssertions;
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
        // Act
        var act = () => Book.New(Guid.Empty, Guid.NewGuid(), "valid_title", "valid_author", 1, true);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ThrowsArgumentNullException_IfOwnerIdIsAnEmptyGuid()
    {
        // Act
        var act = () => Book.New(Guid.NewGuid(), Guid.Empty, "title", "author", 1, true);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void New_ThrowsArgumentNullException_IfTitleIsNullOrEmptyOrWhiteSpaces(string invalidTitle)
    {
        // Act
        var act = () => Book.New(Guid.NewGuid(), Guid.NewGuid(), invalidTitle, "author", 1, true);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void New_ThrowsArgumentNullException_IfAuthorIsNullOrEmptyOrWhiteSpaces(string invalidAuthor)
    {
        // Act
        var act = () => Book.New(Guid.NewGuid(), Guid.NewGuid(), "title", invalidAuthor, 1, true);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void New_ThrowsArgumentOutOfRangeException_IfPagesAreLessThanOne(int invalidPages)
    {
        // Act
        var act = () => Book.New(Guid.NewGuid(), Guid.NewGuid(), "title", "author", invalidPages, true);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
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
        book.Id.Should().Be(bookId);
        book.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        book.OwnerId.Should().Be(ownerId);
        book.Title.Should().Be("title");
        book.Author.Should().Be("author");
        book.Pages.Should().Be(1);
        book.SharedByOwner.Should().BeTrue();
        book.Labels.Should().BeEquivalentTo(new[] { "label1", "label2" });
        book.RequestStatus().Should().BeNull();
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
        book.Labels.Should().BeEquivalentTo(new[] { "label1" });
    }

    [Test]
    public void Update_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
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
            new string[] { "label1" });

        // Act
        var act = () => book.Update(
            notOwnerUserId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" }
        );

        // Assert
        act.Should().Throw<UserIsNotBookOwnerException>();
    }

    [Test]
    public void Update_UpdateFields_IfValidInputs()
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
            new string[] { "label1" });

        // Act
        book.Update(
           ownerId,
           "title_update",
           "author_update",
           2,
           false,
           new string[] { "label2" });

        book.Title.Should().Be("title_update");
        book.Author.Should().Be("author_update");
        book.Pages.Should().Be(2);
        book.SharedByOwner.Should().BeFalse();
        book.Labels.Should().BeEquivalentTo(new[] { "label2" });
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

        // Act
        var act = () => book.Update(ownerId, "title", "author", 50, sharedByOwner, new string[] { "label1" });

        // Assert
        act.Should().Throw<RemoveSharingWithCurrentLoanRequestException>();
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

        // Act
        var act = () => book.RequestNewLoan(Guid.NewGuid());

        // Assert
        act.Should().Throw<BookNotSharedByOwnerException>();
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

        // Act
        var act = () => book.RequestNewLoan(ownerId);

        // Assert
        act.Should().Throw<BookOwnerCannotMakeALoanRequest>();
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
        book.RequestStatus().Should().Be(LoanRequestStatus.WAITING_FOR_ACCEPTANCE);
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

        // Act
        var act = () => book.RefuseLoanRequest(notOwnerUserId);

        // Assert
        act.Should().Throw<UserIsNotBookOwnerException>();
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

        // Act
        var act = () => book.RefuseLoanRequest(ownerId);

        // Assert
        act.Should().Throw<NonExistingLoanRequestException>();
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

        // Act
        var act = () => book.AcceptLoanRequest(ownerId);

        // Assert
        act.Should().Throw<LoanRequestAlreadyAcceptedException>();
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
        book.RequestStatus().Should().BeNull();
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

        // Act
        var act = () => book.AcceptLoanRequest(notOwnerUserId);

        // Assert
        act.Should().Throw<UserIsNotBookOwnerException>();
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

        // Act
        var act = () => book.AcceptLoanRequest(ownerId);

        // Assert
        act.Should().Throw<NonExistingLoanRequestException>();
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

        // Act & Assert
        book.AcceptLoanRequest(ownerId);

        var events = book.ReleaseEvents();

        book.RequestStatus().Should().Be(LoanRequestStatus.ACCEPTED);
        events.Should().ContainSingle(e => e.GetType() == typeof(LoanRequestAcceptedEvent));

        var @event = ((LoanRequestAcceptedEvent)events.First());

        @event.BookId.Should().Be(book.Id);
        @event.DateOcurred.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }
}