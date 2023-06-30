using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Infrastructure.Queries;

public class BookQueries : IBookQueries
{
    private readonly AppDbContext _ctx;

    public BookQueries(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<BookVM>> GetBooksByTitleAsync(string title)
    {
        var queryBuilder = new StringBuilder();
        var queryParam = new DynamicParameters();

        queryBuilder.Append(@"
        select
        books.id,
        owner_id,
        title,
        author,
        pages,
        labels,
        shared_by_owner ,
        requesting_user.email as requesting_user_email,
        current_loan_request_status as request_status
        from books
        	left join users as owner on owner.id = owner_id
        	left join users as requesting_user on requesting_user.id = current_loan_request_requesting_user_id
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

    public async Task<BookVM> GetBookByIdAsync(Guid id)
    {
        var queryParam = new DynamicParameters();

        string query = @"
        select
        books.id,
        owner_id,
        title,
        author,
        pages,
        labels,
        shared_by_owner ,
        requesting_user.email as requesting_user_email,
        current_loan_request_status as request_status
        from books
        	left join users as owner on owner.id = owner_id
        	left join users as requesting_user on requesting_user.id = current_loan_request_requesting_user_id
        where books.id = @Id";

        using var connection = _ctx.Database.GetDbConnection();
        queryParam.Add("Id", id);
        return await connection.QueryFirstOrDefaultAsync<BookVM>(
            sql: query,
            param: queryParam
        );
    }
}