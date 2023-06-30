using System.Text.Json.Serialization;
using MediatR;

namespace ShareBook.Application.Books.ViewModels;

public class BookVM
{
    public Guid Id { get; set; }
    public string Owner { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public string Labels { get; set; }
    public bool SharedByOwner { get; set; }
    public int? RequestStatus { get; set; }
    public string RequestingUserEmail { get; set; }
}