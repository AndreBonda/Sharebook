using ShareBook.Domain.Shared.Primitives;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.Domain.Users;

public class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    private readonly Password _password;

    public User(Guid id, Email email, Password password) : base(id)
    {
        Email = email;
        _password = password;

        Validate();
    }

    protected User()
    {}

    public bool Authenticate(string plainTextPassword) => _password.VerifyPassword(plainTextPassword);

    private void Validate() {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
        ArgumentNullException.ThrowIfNull(Email, nameof(Email));
        ArgumentNullException.ThrowIfNull(_password, nameof(_password));
    }
}