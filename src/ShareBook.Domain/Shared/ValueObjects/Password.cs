using System.Text.RegularExpressions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shared.ValueObjects;

public class Password : ValueObject
{
    public const int MIN_LENGTH = 6;
    public const string VALUE_REGEX = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$";

    public string PasswordHash { get; private set; }

    public Password(string plainTextPassword, IHashingProvider hashingProvider)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword) || plainTextPassword.Length < MIN_LENGTH ||
            !Regex.IsMatch(plainTextPassword, VALUE_REGEX))
        {
            throw new ArgumentException(nameof(plainTextPassword));
        }

        ArgumentNullException.ThrowIfNull(hashingProvider);

        PasswordHash = hashingProvider.Hash(plainTextPassword);
    }

    protected Password()
    {
        // Useful for mocking
    }

    public virtual bool VerifyPassword(string plainTextPassword, IHashingProvider hashingProvider) => 
        hashingProvider.Verify(plainTextPassword, PasswordHash);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
    }
}