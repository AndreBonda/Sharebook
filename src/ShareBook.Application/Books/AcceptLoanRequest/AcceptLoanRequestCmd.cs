using MediatR;

namespace ShareBook.Application.Books;

public record AcceptLoanRequestCmd(Guid BookId, Guid LoanRequestId, Guid UserId) : IRequest;