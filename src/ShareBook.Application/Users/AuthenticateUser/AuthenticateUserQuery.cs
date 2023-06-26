using MediatR;

namespace ShareBook.Application.Users;

public record AuthenticateUserQuery(string Email, string PlainTextPassword) : IRequest<string>;