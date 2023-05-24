using MediatR;

namespace ShareBook.Application.Books;

public record RefuseLoanRequestCmd(
    Guid BookId,
    string RequestingUser
    ) : IRequest;