using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShareBook.API.DTOs;
using ShareBook.Application.Books.CreateBook;
using ShareBook.Application.Books.GetBooks;
using ShareBook.Application.Books.UpdateBook;

namespace ShareBook.API.Controllers;

[ApiController]
[Route("api")]
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
    public Task<IActionResult> GetById(Guid id)
    {
        throw new NotImplementedException();
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

    [HttpPatch("book/{book_id}/loan_request/{current_user}")]
    public async Task<IActionResult> RequestLoan(
        [FromRoute(Name = "book_id")] Guid BookId,
        [FromRoute(Name = "current_user")] string CurrentUser)
    {
        await _mediator.Send(new CreateLoanRequestCmd(BookId, CurrentUser));

        return CreatedAtAction(nameof(GetById), new { id = BookId }, null);
    }

}
