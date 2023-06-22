using MediatR;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shared.ValueObjects;
using ShareBook.Domain.Users;

namespace ShareBook.Application.Users;

public class RegisterUserHandler : IRequestHandler<RegisterUserCmd>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingProvider _hashingProvider;

    public RegisterUserHandler(IUserRepository userRepository, IHashingProvider hashingProvider)
    {
        _userRepository = userRepository;
        _hashingProvider = hashingProvider;
    }
    
    public async Task Handle(RegisterUserCmd request, CancellationToken cancellationToken)
    {
        var prova = await _userRepository.GetByIdAsync(Guid.Parse("1adf8dfc-2cae-4093-893e-ba9e0381db9f"));
        if(await _userRepository.GetByEmailAsync(request.Email) is not null)
            throw new BadRequestException("Email already used");

        User user = new(Guid.NewGuid(),
            new Email(request.Email),
            new Password(request.Password, _hashingProvider));

        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
    }
}