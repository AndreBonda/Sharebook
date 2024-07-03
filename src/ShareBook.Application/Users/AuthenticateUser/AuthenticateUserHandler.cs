using MediatR;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Users;
using ShareBook.Domain.Users.Exceptions;

namespace ShareBook.Application.Users;

public class AuthenticateUserHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IHashingProvider hashingProvider)
    : IRequestHandler<AuthenticateUserQuery, string>
{
    public async Task<string> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null ||
            !user.Authenticate(request.PlainTextPassword, hashingProvider))
        {
            throw new BadCredentialException("Wrong email or password");
        }

        return jwtProvider.CreateToken(user.Id, request.Email);
    }
}