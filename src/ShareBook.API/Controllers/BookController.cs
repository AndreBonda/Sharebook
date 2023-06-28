using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareBook.API.DTOs;
using ShareBook.Application.Books;
using ShareBook.Api.Extensions;

namespace ShareBook.API.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IMediator _mediator;

    public BookController(ILogger<BookController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("book/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var book = await _mediator.Send(new GetBookQuery(id));
        
        return Ok(book);
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetAll(string title)
    {
        var books = await _mediator.Send(new GetBooksQuery(
            title
        ));

        return Ok(books);
    }

    [HttpPost("book")]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var id = Guid.NewGuid();

        await _mediator.Send(new CreateBookCmd(
            id,
            dto.CurrentUser,
            dto.Title,
            dto.Author,
            dto.Pages,
            dto.SharedByOwner,
            dto.Labels
        ));

        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpPatch("book/{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateBookDto dto)
    {
        await _mediator.Send(new UpdateBookCmd(
            id,
            dto.CurrentUser,
            dto.Title,
            dto.Author,
            dto.Pages,
            dto.SharedByOwner,
            dto.Labels
        ));

        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpPatch("book/{book_id}/new_loan_request/{current_user}")]
    public async Task<IActionResult> RequestLoan(
        [FromRoute(Name = "book_id")] Guid BookId,
        [FromRoute(Name = "current_user")] string CurrentUser)
    {
        await _mediator.Send(new CreateLoanRequestCmd(BookId, CurrentUser));

        return CreatedAtAction(nameof(GetById), new { id = BookId }, null);
    }

    [HttpPatch("book/{book_id}/refuse_loan_request/{current_user}")]
    public async Task<IActionResult> RefuseLoanRequest(
        [FromRoute(Name = "book_id")] Guid BookId,
        [FromRoute(Name = "current_user")] string CurrentUser)
    {
        await _mediator.Send(new RefuseLoanRequestCmd(BookId, CurrentUser));

        return CreatedAtAction(nameof(GetById), new { id = BookId }, null);
    }

    [HttpPatch("book/{book_id}/accept_loan_request/{current_user}")]
    public async Task<IActionResult> AcceptLoanRequest(
        [FromRoute(Name = "book_id")] Guid bookId,
        [FromRoute(Name = "current_user")] string currentUser)
    {
        await _mediator.Send(new AcceptLoanRequestCmd(bookId, currentUser));

        return CreatedAtAction(nameof(GetById), new { id = bookId }, null);
    }

}
