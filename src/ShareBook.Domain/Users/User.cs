using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Primitives;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.Domain.Users;

public class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    public User(Guid id, Email email, Password password) : base(id)
    {
        Email = email;
        Password = password;

        Validate();
    }

    public bool Authenticate(string plainTextPassword, IHashingProvider hashingProvider) =>
        Password.VerifyPassword(plainTextPassword, hashingProvider);

    private void Validate() {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
    }
}