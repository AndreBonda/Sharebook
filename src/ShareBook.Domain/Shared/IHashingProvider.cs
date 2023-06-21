namespace ShareBook.Domain.Shared
{
    public interface IHashingProvider
    {
        bool CheckHash(string plainTextPassword, byte[] passwordSalt, byte[] passwordHash);
        (byte[] Salt, byte[] Hash) Hash(string plainTextPassword);
    }
}