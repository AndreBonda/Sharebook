using ShareBook.Domain.Books.Exceptions;
using ShareBook.Domain.Shared;

namespace ShareBook.Domain.Books;

public class LoanRequest : Entity<Guid>
{
    public enum LoanRequestStatus
    {
        WAITING_FOR_ACCEPTANCE,
        ACCEPTED
    }

    private LoanRequest(Guid id, string requestingUser) : base(id, DateTime.UtcNow)
    {
        RequestingUser = requestingUser;
        Status = LoanRequestStatus.WAITING_FOR_ACCEPTANCE;
    }

    public LoanRequestStatus Status { get; private set; }
    public string RequestingUser { get; private set; }

    public static LoanRequest New(
        Guid id,
        string requestingUser
    )
    {
        return new LoanRequest(id, requestingUser);
    }
}