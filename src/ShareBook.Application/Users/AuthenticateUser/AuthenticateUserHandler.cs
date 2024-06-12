using MediatR;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Users;
using ShareBook.Domain.Users.Exceptions;

namespace ShareBook.Application.Users;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashingProvider _hashingProvider;

    public AuthenticateUserHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHashingProvider hashingProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashingProvider = hashingProvider;
    }

    public async Task<string> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null ||
            !user.Authenticate(request.PlainTextPassword, _hashingProvider))
        {
            throw new BadCredentialException("Wrong email or password");
        }

        return _jwtProvider.CreateToken(user.Id, request.Email);
    }
}