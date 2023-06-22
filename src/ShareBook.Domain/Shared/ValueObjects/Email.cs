using System.Text.RegularExpressions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shared.ValueObjects;

public class Email : ValueObject
{
    public const string VALUE_REGEX = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public virtual string Value { get; private set; }

    public Email(string email)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(email);

        if (!Regex.IsMatch(email, VALUE_REGEX))
            throw new ArgumentException(nameof(email));

        Value = email;
    }

    protected Email() {
        // Useful for mocking
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}