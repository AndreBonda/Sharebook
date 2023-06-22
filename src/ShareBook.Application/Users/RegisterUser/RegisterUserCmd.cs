using MediatR;

namespace ShareBook.Application.Users;

public record RegisterUserCmd(string Email, string Password) : IRequest;