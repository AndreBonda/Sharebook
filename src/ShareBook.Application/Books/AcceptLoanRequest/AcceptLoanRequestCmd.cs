using MediatR;

namespace ShareBook.Application.Books;

public record AcceptLoanRequestCmd(Guid BookId, string BookOwner) : IRequest;