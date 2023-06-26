namespace ShareBook.Domain.Shared
{
    public interface IJwtProvider
    {
        string CreateToken(Guid userId, string email);
    }
}