using MediatR;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shared.ValueObjects;
using ShareBook.Domain.Users;

namespace ShareBook.Application.Users;

public class RegisterUserHandler(IUserRepository userRepository, IHashingProvider hashingProvider)
    : IRequestHandler<RegisterUserCmd>
{
    public async Task Handle(RegisterUserCmd request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByEmailAsync(request.Email) is not null)
            throw new BadRequestException("Email already used");

        User user = new(
            Guid.NewGuid(),
            new Email(request.Email),
            new Password(request.Password, hashingProvider)
        );

        await userRepository.AddAsync(user);
    }
}