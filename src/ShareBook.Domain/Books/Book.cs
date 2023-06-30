using ShareBook.Domain.Books.Events;
using ShareBook.Domain.Books.Exceptions;
using ShareBook.Domain.Shared.Primitives;
using static ShareBook.Domain.Books.LoanRequest;

namespace ShareBook.Domain.Books;

public class Book : AggregateRoot<Guid>
{
    private List<string> _labels = new();
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Pages { get; private set; }
    public IReadOnlyCollection<string> Labels => _labels;
    public bool SharedByOwner { get; private set; }
    private LoanRequest CurrentLoanRequest { get; set; }

    protected Book(
        Guid id,
        Guid ownerId,
        string title,
        string author,
        int pages,
        bool sharedByOwner,
        IEnumerable<string> labels) : base(id)
    {
        OwnerId = ownerId;
        Title = title;
        Author = author;
        Pages = pages;
        SharedByOwner = sharedByOwner;
        CurrentLoanRequest = null;
        SetupLabels(labels);

        Validate();
    }

    protected Book()
    { }

    public virtual void Update(Guid bookOwnerId, string title, string author, int pages, bool sharedByOwner, IEnumerable<string> labels)
    {
        if (bookOwnerId != OwnerId)
            throw new UserIsNotBookOwnerException($"User {bookOwnerId} is not the owner of this book {Id}");

        Title = title;
        Author = author;
        Pages = pages;
        SharedByOwner = sharedByOwner;
        SetupLabels(labels);

        Validate();
    }

    public void RequestNewLoan(Guid requestingUserId)
    {
        if (!SharedByOwner)
            throw new BookNotSharedByOwnerException($"This book {Id} is not shared");

        if (requestingUserId == OwnerId)
            throw new BookOwnerCannotMakeALoanRequest($"User {requestingUserId} is already the owner of this book {Id}");

        if (CurrentLoanRequest is not null)
        {
            throw new LoanRequestAlreadyExistsException($"This book {Id} has already a loan request");
        }

        CurrentLoanRequest = LoanRequest.New(Guid.NewGuid(), requestingUserId);
    }

    public void RefuseLoanRequest(Guid bookOwnerId)
    {
        if (bookOwnerId != OwnerId)
            throw new UserIsNotBookOwnerException($"User {bookOwnerId} is not the owner of this book {Id}");

        if (CurrentLoanRequest is null)
            throw new NonExistingLoanRequestException($"There is not any loan request for this book {Id}");

        if (CurrentLoanRequest.IsAccepted())
            throw new LoanRequestAlreadyAcceptedException($"This loan request is already accepted");

        CurrentLoanRequest = null;
    }

    public virtual void AcceptLoanRequest(Guid bookOwnerId)
    {
        if (bookOwnerId != OwnerId)
            throw new UserIsNotBookOwnerException($"User {bookOwnerId} is not the owner of this book {Id}");

        if (CurrentLoanRequest is null)
            throw new NonExistingLoanRequestException($"There is not any loan request for this book {Id}");

        CurrentLoanRequest.Accept();

        RaiseEvent(new LoanRequestAcceptedEvent { BookId = Id });
    }

    private void SetupLabels(IEnumerable<string> labels)
    {
        _labels = new();

        foreach (var label in labels)
            AddLabel(label);
    }

    private void AddLabel(string label)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentNullException(nameof(label));

        if (!_labels.Contains(label))
            _labels.Add(label);
    }

    public LoanRequestStatus? RequestStatus() => CurrentLoanRequest?.Status;

    private void Validate()
    {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
        if (OwnerId == Guid.Empty) throw new ArgumentNullException(nameof(OwnerId));
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentNullException(nameof(Title));
        if (string.IsNullOrWhiteSpace(Author)) throw new ArgumentNullException(nameof(Author));
        if (Pages <= 0) throw new ArgumentOutOfRangeException(nameof(Pages));
        if (SharedByOwner is false && CurrentLoanRequest is not null)
            throw new RemoveSharingWithCurrentLoanRequestException($"This book {Id} has a loan request in progess.");
    }

    public static Book New(
        Guid id,
        Guid ownerId,
        string title,
        string author,
        int pages,
        bool sharedByOwner,
        IEnumerable<string> labels = null)
    {
        return new Book(id, ownerId, title, author, pages, sharedByOwner, labels ?? Enumerable.Empty<string>());
    }
}