using System.Text.RegularExpressions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Shared.ValueObjects;

public class Password : ValueObject
{
    public const int MIN_LENGTH = 6;
    public const string VALUE_REGEX = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$";

    private readonly IHashingProvider _hashingProvider;

    public virtual byte[] PasswordSalt { get; private set; }
    public virtual byte[] PasswordHash { get; private set; }

    public Password(string plainTextPassword, IHashingProvider hashingProvider)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword) || plainTextPassword.Length < MIN_LENGTH ||
            !Regex.IsMatch(plainTextPassword, VALUE_REGEX))
        {
            throw new ArgumentException(nameof(plainTextPassword));
        }

        ArgumentNullException.ThrowIfNull(hashingProvider);

        _hashingProvider = hashingProvider;
        HashPassword(plainTextPassword);
    }

    public Password(byte[] passwordSalt, byte[] passwordHash, IHashingProvider hashingProvider)
    {
        if (passwordSalt == null || passwordSalt.Length == 0)
            throw new ArgumentException(nameof(passwordSalt));

        if (passwordHash == null || passwordHash.Length == 0)
            throw new ArgumentException(nameof(passwordHash));

        if (hashingProvider == null)
            throw new ArgumentException(nameof(hashingProvider));

        _hashingProvider = hashingProvider;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
        yield return PasswordSalt;
    }

    public bool IsPasswordCorrect(string plainTextPassword) => _hashingProvider.CheckHash(plainTextPassword, PasswordSalt, PasswordHash);

    private void HashPassword(string plainTextPassword)
    {
        (byte[] Salt, byte[] Hash) hashingResult = _hashingProvider.Hash(plainTextPassword);
        PasswordSalt = hashingResult.Salt;
        PasswordHash = hashingResult.Hash;
    }
}