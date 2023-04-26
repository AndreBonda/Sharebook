using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShareBook.API.DTOs;
using ShareBook.Application.Books.CreateBook;
using ShareBook.Application.Books.GetBooks;

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
            dto.Owner,
            dto.Title,
            dto.Author,
            dto.Pages,
            dto.Labels
        ));

        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }
}
