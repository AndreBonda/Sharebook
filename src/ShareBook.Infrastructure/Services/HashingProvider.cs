using ShareBook.Domain.Shared;

namespace ShareBook.Infrastructure.Services;

public sealed class HashingProvider : IHashingProvider
{
    public string Hash(string plainTextValue)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainTextValue);
    }
}