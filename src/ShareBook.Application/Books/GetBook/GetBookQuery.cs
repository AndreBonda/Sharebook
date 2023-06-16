using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public record GetBookQuery(Guid Id) : IRequest<BookVM>;