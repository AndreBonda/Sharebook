using ShareBook.Domain.Books.Events;
using ShareBook.Domain.Books.Exceptions;
using ShareBook.Domain.Shared.Primitives;
using static ShareBook.Domain.Books.LoanRequest;

namespace ShareBook.Domain.Books;

public sealed class Book : AggregateRoot<Guid>
{
    private string[] _labels;
    private readonly List<LoanRequest> _currentLoanRequests;
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public uint Pages { get; private set; }
    public bool SharedByOwner { get; private set; }
    public string[] Labels => _labels.ToArray();
    public (Guid Id, Guid RequestingUserId)[] CurrentLoanRequests =>
        _currentLoanRequests
            .Select(lr => (lr.Id, lr.RequestingUserId))
            .ToArray();

    public Book(
        Guid id,
        Guid ownerId,
        string title,
        string author,
        uint pages,
        bool sharedByOwner,
        IEnumerable<string>? labels = null,
        IEnumerable<LoanRequest>? loanRequests = null) : base(id)
    {
        OwnerId = ownerId;
        Title = title;
        Author = author;
        Pages = pages;
        SharedByOwner = sharedByOwner;
        _labels = labels?.ToArray() ?? [];
        _currentLoanRequests = loanRequests?.ToList() ?? new();

        Validate();
    }

    public void Update(Guid bookOwnerId, string title, string author, uint pages, bool sharedByOwner, IEnumerable<string>? labels)
    {
        if (bookOwnerId != OwnerId)
            throw new UserIsNotBookOwnerException($"User {bookOwnerId} is not the owner of this book {Id}");

        Title = title;
        Author = author;
        Pages = pages;
        SharedByOwner = sharedByOwner;
        _labels = labels?.ToArray() ?? [];

        Validate();
    }

    public void RequestNewLoan(Guid requestingUserId)
    {
        if (!SharedByOwner)
            throw new BookNotSharedByOwnerException($"This book {Id} is not shared");

        if (HasAnAcceptedLoanRequest())
            throw new LoanRequestAlreadyAcceptedException($"The book with ID {Id} already has an accepted loan request");

        if (requestingUserId == OwnerId)
            throw new BookOwnerCannotMakeALoanRequest($"User {requestingUserId} is already the owner of this book {Id}");

        if (_currentLoanRequests.Any(lr => lr.RequestingUserId == requestingUserId))
        {
            throw new LoanRequestAlreadyExistsForUserException(
                $"A loan request for user ID {{requestingUserId}} already exists");
        }

        _currentLoanRequests.Add(new(Guid.NewGuid(), requestingUserId));
    }

    public void AcceptLoanRequest(Guid bookOwnerId, Guid loanRequestId)
    {
        if (bookOwnerId != OwnerId)
            throw new UserIsNotBookOwnerException($"User {bookOwnerId} is not the owner of this book {Id}");

        if (HasAnAcceptedLoanRequest())
            throw new LoanRequestAlreadyAcceptedException($"The book with ID {Id} already has an accepted loan request");

        LoanRequest? request = _currentLoanRequests.FirstOrDefault(lr => lr.Id == loanRequestId);

        if (request is null)
            throw new NonExistingLoanRequestException($"There is no loan request with this ID {Id}");

        request.Accept();

        RaiseEvent(new LoanRequestAcceptedEvent { BookId = Id });
    }

    public bool HasAnAcceptedLoanRequest() => _currentLoanRequests.Any(lr => lr.Status == LoanRequestStatus.Accepted);

    private void Validate()
    {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
        if (OwnerId == Guid.Empty) throw new ArgumentNullException(nameof(OwnerId));
        ArgumentException.ThrowIfNullOrWhiteSpace(Title);
        ArgumentException.ThrowIfNullOrWhiteSpace(Author);
        if (SharedByOwner is false && HasAnAcceptedLoanRequest())
            throw new RemoveSharingWithAnAcceptedLoanRequestException($"This book with ID {Id} has a loan request in progess.");
    }
}