using MediatR;

namespace ShareBook.Application.Books.CreateBook;

public record RefuseLoanRequestCmd(
    Guid BookId,
    string RequestingUser
    ) : IRequest;