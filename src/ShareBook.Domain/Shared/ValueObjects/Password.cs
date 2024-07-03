using System.Text.RegularExpressions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shared.ValueObjects;

public sealed class Password : ValueObject
{
    public const int MinLength = 6;
    public const string ValueRegex = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$";

    public string PasswordHash { get; private set; }

    public Password(string plainTextPassword, IHashingProvider hashingProvider)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword) || plainTextPassword.Length < MinLength ||
            !Regex.IsMatch(plainTextPassword, ValueRegex))
        {
            throw new ArgumentException(nameof(plainTextPassword));
        }

        PasswordHash = hashingProvider.Hash(plainTextPassword);
    }

    public Password(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public bool VerifyPassword(string plainTextPassword, IHashingProvider hashingProvider) =>
        hashingProvider.Verify(plainTextPassword, PasswordHash);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
    }
}