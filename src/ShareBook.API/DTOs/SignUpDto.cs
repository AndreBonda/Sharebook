using System.ComponentModel.DataAnnotations;

namespace ShareBook.API.DTOs;

public class SignUpDto
{
    [Required]
    [RegularExpression(ShareBook.Domain.Shared.ValueObjects.Email.VALUE_REGEX)]
    public string Email { get; set; }

    [Required]
    [MinLength(ShareBook.Domain.Shared.ValueObjects.Password.MIN_LENGTH)]
    [RegularExpression(ShareBook.Domain.Shared.ValueObjects.Password.VALUE_REGEX)]
    public string Password { get; set; }
}