using MediatR;

namespace ShareBook.Application.Books.CreateBook;

public record AcceptLoanRequestCmd(
    Guid BookId,
    string BookOwner
    ) : IRequest;