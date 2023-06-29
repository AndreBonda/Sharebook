using ShareBook.Domain.Books.Exceptions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Books;

public class LoanRequest : Entity<Guid>
{
    public enum LoanRequestStatus
    {
        WAITING_FOR_ACCEPTANCE,
        ACCEPTED
    }

    private LoanRequest(Guid id, Guid requestingUserId) : base(id)
    {
        RequestingUserId = requestingUserId;
        Status = LoanRequestStatus.WAITING_FOR_ACCEPTANCE;
    }

    public LoanRequestStatus Status { get; private set; }
    public Guid RequestingUserId { get; private set; }

    public bool IsAccepted() => Status == LoanRequestStatus.ACCEPTED;

    public void Accept() {
        if(Status is LoanRequestStatus.ACCEPTED)
            throw new LoanRequestAlreadyAcceptedException($"This loan request is already accepted");

        Status = LoanRequestStatus.ACCEPTED;
    }

    public static LoanRequest New(
        Guid id,
        Guid requestingUserId
    )
    {
        return new LoanRequest(id, requestingUserId);
    }
}