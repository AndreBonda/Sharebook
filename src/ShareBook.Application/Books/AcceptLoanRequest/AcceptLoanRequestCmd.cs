using MediatR;

namespace ShareBook.Application.Books;

public record AcceptLoanRequestCmd(Guid BookId, Guid UserId) : IRequest;