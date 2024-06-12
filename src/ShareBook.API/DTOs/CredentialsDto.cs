using System.ComponentModel.DataAnnotations;

namespace ShareBook.API.DTOs;

public class CreadentialsDto
{
    [Required]
    [RegularExpression(ShareBook.Domain.Shared.ValueObjects.Email.VALUE_REGEX)]
    public required string Email { get; set; }

    [Required]
    [MinLength(ShareBook.Domain.Shared.ValueObjects.Password.MIN_LENGTH)]
    [RegularExpression(ShareBook.Domain.Shared.ValueObjects.Password.VALUE_REGEX)]
    public required string Password { get; set; }
}