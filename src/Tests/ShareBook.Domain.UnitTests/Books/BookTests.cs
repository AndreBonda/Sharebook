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
        var act = () => new Book(Guid.Empty, Guid.NewGuid(), "valid_title", "valid_author", 1, true);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ThrowsArgumentNullException_IfOwnerIdIsAnEmptyGuid()
    {
        // Act
        var act = () => new Book(Guid.NewGuid(), Guid.Empty, "title", "author", 1, true);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    public void New_ThrowsArgumentNullException_IfTitleIsNullOrEmptyOrWhiteSpaces(string invalidTitle)
    {
        // Act
        var act = () => new Book(Guid.NewGuid(), Guid.NewGuid(), invalidTitle, "author", 1, true);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    public void New_ThrowsArgumentNullException_IfAuthorIsNullOrEmptyOrWhiteSpaces(string invalidAuthor)
    {
        // Act
        var act = () => new Book(Guid.NewGuid(), Guid.NewGuid(), "title", invalidAuthor, 1, true);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_CreateNewBook_IfValidInputs()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();

        // Act
        var book = new Book(
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
        book.HasAnAcceptedLoanRequest().Should().BeFalse();
    }

    [Test]
    public void Update_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var notOwnerUserId = Guid.NewGuid();
        var book = new Book(
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
        var book = new Book(
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
    public void Update_ThrowsRemoveSharingWithAnAcceptedLoanRequestException_IfShareByOwnerIsSetToFalseWhileAnAcceptedLoanRequestExists()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

        book.RequestNewLoan(Guid.NewGuid());
        var loanRequestId = book.CurrentLoanRequests.First().Id;
        book.AcceptLoanRequest(ownerId, loanRequestId);

        // Act
        bool sharedByOwner = false;
        var act = () => book.Update(ownerId, "title", "author", 50, sharedByOwner, new string[] { "label1" });

        // Assert
        act.Should().Throw<RemoveSharingWithAnAcceptedLoanRequestException>();
    }

    [Test]
    public void RequestNewLoan_ThrowsBookNotSharedByOwnerException_IfBookNotSharedByOwner()
    {
        // Arrange
        bool sharedByOwner = false;

        var book = new Book(
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
    public void RequestNewLoan_ThrowsLoanRequestAlreadyAcceptedException_IfAnAcceptedLoanRequestIsPresent()
    {
        // Arrange
        Guid requestingUserOne = Guid.NewGuid();
        Guid requestingUserTwo = Guid.NewGuid();
        Guid ownerId = Guid.NewGuid();
        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
        );

        // Act
        book.RequestNewLoan(requestingUserOne);
        var loanRequestId = book.CurrentLoanRequests.First().Id;
        book.AcceptLoanRequest(ownerId, loanRequestId);
        var act = () => book.RequestNewLoan(requestingUserTwo);

        // Assert
        act.Should().Throw<LoanRequestAlreadyAcceptedException>();
    }

    [Test]
    public void RequestNewLoan_ThrowsBookOwnerCannotMakeALoanRequest_IfOwnerMakeARequestForHisBook()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        var book = new Book(
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
    public void RequestNewLoan_ThrowsLoanRequestAlreadyExistsForUserException_IfRequestAlreadyExists()
    {
        // Arrange
        Guid requestUserId = Guid.NewGuid();
        var book = new Book(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
        );

        // Act
        book.RequestNewLoan(requestUserId);
        var act = () => book.RequestNewLoan(requestUserId);

        // Assert
        act.Should().Throw<LoanRequestAlreadyExistsForUserException>();
    }

    [Test]
    public void RequestNewLoan_SetCurrentLoanRequestStatus_IfLoanRequestIsCreated()
    {
        // Arrange
        var book = new Book(
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
        book.HasAnAcceptedLoanRequest().Should().BeFalse();
    }

    [Test]
    public void AcceptLoanRequest_ThrowsUserIsNotBookOwnerException_IfUserIsNotTheBookOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();
        var notOwnerUserId = Guid.NewGuid();

        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        // Act
        book.RequestNewLoan(requestingUserId);
        Guid loanRequestId = book.CurrentLoanRequests.First().Id;
        var act = () => book.AcceptLoanRequest(notOwnerUserId, loanRequestId);

        // Assert
        act.Should().Throw<UserIsNotBookOwnerException>();
    }

    [Test]
    public void AcceptLoanRequest_ThrowsNonExistingLoanRequestException_IfLoanRequestDoesNotExist()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        // Act
        var act = () => book.AcceptLoanRequest(ownerId, Guid.NewGuid());

        // Assert
        act.Should().Throw<NonExistingLoanRequestException>();
    }

    [Test]
    public void AcceptLoanRequest_ThrowsLoanRequestAlreadyAcceptedException_IfExistsAnAcceptedLoanRequest()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserIdOne = Guid.NewGuid();
        var requestingUserIdTwo = Guid.NewGuid();
        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
        );

        // Act
        book.RequestNewLoan(requestingUserIdOne);
        book.RequestNewLoan(requestingUserIdTwo);
        Guid loanRequestIdOne = book.CurrentLoanRequests[0].Id;
        Guid loanRequestIdTwo = book.CurrentLoanRequests[1].Id;

        book.AcceptLoanRequest(ownerId, loanRequestIdOne);

        var act = () => book.AcceptLoanRequest(ownerId, loanRequestIdTwo);

        // Assert
        act.Should().Throw<LoanRequestAlreadyAcceptedException>();
    }

    [Test]
    public void AcceptLoanRequest_UpdatesBook_IfLoanRequestIsAccepted()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var book = new Book(
            Guid.NewGuid(),
            ownerId,
            "title",
            "author",
            1,
            true,
            new string[] { "label" }
            );

        // Act
        book.RequestNewLoan(requestingUserId);
        Guid loanRequestId = book.CurrentLoanRequests.First().Id;
        book.AcceptLoanRequest(ownerId, loanRequestId);
        book.HasAnAcceptedLoanRequest().Should().BeTrue();

        var events = book.ReleaseEvents();
        events.Should().ContainSingle(e => e.GetType() == typeof(LoanRequestAcceptedEvent));

        var @event = ((LoanRequestAcceptedEvent)events.First());

        @event.BookId.Should().Be(book.Id);
        @event.DateOcurred.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }
}