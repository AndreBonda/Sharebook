using MediatR;

namespace ShareBook.Application.Books;

public record CreateLoanRequestCmd(
    Guid BookId,
    string RequestingUser
    ) : IRequest;