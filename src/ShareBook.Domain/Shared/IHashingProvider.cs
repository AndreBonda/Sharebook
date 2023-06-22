namespace ShareBook.Domain.Shared
{
    public interface IHashingProvider
    {
        string Hash(string plainTextValue);
    }
}