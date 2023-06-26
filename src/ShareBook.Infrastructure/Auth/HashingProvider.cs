using ShareBook.Domain.Shared;

namespace ShareBook.Infrastructure.Auth;

public sealed class HashingProvider : IHashingProvider
{
    public string Hash(string plainTextValue)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainTextValue);
    }

    public bool Verify(string plainTextValue, string hashedValue) {
        return BCrypt.Net.BCrypt.Verify(plainTextValue, hashedValue);
    }
}