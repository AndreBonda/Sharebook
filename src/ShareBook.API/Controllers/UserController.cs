using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShareBook.API.DTOs;
using ShareBook.Application.Books;
using ShareBook.Application.Users;

namespace ShareBook.API.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("user/sign-up")]
    public async Task<IActionResult> SignUp(SignUpDto @params)
    {
        await _mediator.Send(new RegisterUserCmd(@params.Email, @params.Password));

        return Ok("User registration completed.");
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetAll(string title)
    {
        var books = await _mediator.Send(new GetBooksQuery(
            title
        ));

        return Ok(books);
    }
}
