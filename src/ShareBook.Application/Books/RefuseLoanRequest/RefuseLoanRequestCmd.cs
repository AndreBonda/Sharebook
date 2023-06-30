using MediatR;

namespace ShareBook.Application.Books;

public record RefuseLoanRequestCmd(
    Guid BookId,
    Guid UserId
    ) : IRequest;