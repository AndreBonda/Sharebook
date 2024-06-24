using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShareBook.API.DTOs;
using ShareBook.Application.Books;
using ShareBook.Application.Users;

namespace ShareBook.API.Controllers;

[ApiController]
[Route("api")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("user/sign-up")]
    public async Task<IActionResult> SignUp(CreadentialsDto @params)
    {
        await mediator.Send(new RegisterUserCmd(@params.Email, @params.Password));

        return Ok("User registration completed.");
    }

    [HttpPost("user/sign-in")]
    public async Task<IActionResult> GetAll(CreadentialsDto @params)
    {
        string jwt = await mediator.Send(new AuthenticateUserQuery(@params.Email, @params.Password));

        return Ok(jwt);
    }
}
