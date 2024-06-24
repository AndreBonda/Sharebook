using ShareBook.Domain.Books.Exceptions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Books;

public class LoanRequest : Entity<Guid>
{
    public enum LoanRequestStatus
    {
        WaitingForAcceptance,
        Accepted
    }

    public LoanRequestStatus Status { get; private set; }
    public Guid RequestingUserId { get; private init; }

    public LoanRequest(Guid id, Guid requestingUserId) : base(id)
    {
        RequestingUserId = requestingUserId;
        Status = LoanRequestStatus.WaitingForAcceptance;
    }

    public LoanRequest(Guid id, Guid requestingUserId, LoanRequestStatus status) : base(id)
    {
        RequestingUserId = requestingUserId;
        Status = status;
    }

    public void Accept() {
        if(Status is LoanRequestStatus.Accepted)
            throw new LoanRequestAlreadyAcceptedException($"This loan request is already accepted");

        Status = LoanRequestStatus.Accepted;
    }
}