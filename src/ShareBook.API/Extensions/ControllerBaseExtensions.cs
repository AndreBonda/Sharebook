using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ShareBook.Api.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid GetUserId(this ControllerBase controller) {
        return Guid.Parse(controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }

    public static string GetUserEmail(this ControllerBase controller)
    {
        return controller.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
    }
}