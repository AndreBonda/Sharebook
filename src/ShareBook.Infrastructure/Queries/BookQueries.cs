using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Books.GetBooks;

namespace ShareBook.Infrastructure.Queries;

public class BookQueries : IBookQueries
{
    private class UserProva
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    private readonly AppDbContext _ctx;

    public BookQueries(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title)
    {
        // var parameters = new DynamicParameters();
        // parameters.Add("@Username", $"%{title.ToLower()}%");
        // string query = "SELECT * FROM userprova where lower(username) like @Username";

        var queryBuilder = new StringBuilder();
        var queryParam = new DynamicParameters();

        queryBuilder.Append(@"
        select
        id,
        title,
        author,
        pages,
        labels,
        shared_by_owner ,
        current_loan_request_requesting_user,
        current_loan_request_status
        from books
        where 1 = 1");

        if(!string.IsNullOrEmpty(title)) {
            queryBuilder.Append(" and lower(title) like @Title");
            queryParam.Add("Title", $"%{title.ToLower()}%");
        }

        using var connection = _ctx.Database.GetDbConnection();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var books = await connection.QueryAsync<BookVM>(
            sql: queryBuilder.ToString(),
            param: queryParam
        );

        return books;
    }
}