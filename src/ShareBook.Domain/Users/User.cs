using ShareBook.Domain.Shared.Primitives;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.Domain.Users;

public class User : AggregateRoot<Guid>
{
    private readonly Email _email;
    private readonly Password _password;
    public string Email => _email.Value;
    public string Password => _password.PasswordHash;

    public User(Guid id, Email email, Password password) : base(id)
    {
        _email = email;
        _password = password;

        Validate();
    }

    public bool Authenticate(string plainTextPassword) => _password.VerifyPassword(plainTextPassword);

    private void Validate() {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
        ArgumentNullException.ThrowIfNull(_email, nameof(_email));
        ArgumentNullException.ThrowIfNull(_password, nameof(_password));
    }
}