using MediatR;
using Microsoft.Extensions.Logging;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shippings;

namespace ShareBook.Application.Books.CreateBook;

public class AcceptLoanRequestHandler : IRequestHandler<AcceptLoanRequestCmd>
{
    private readonly IBookRepository _bookRepository;
    private readonly IShippingRepository _shippingRepository;
    private readonly AcceptLoanRequestService _acceptLoanRequestService;

    public AcceptLoanRequestHandler(IBookRepository bookRepository, IShippingRepository shippingRepository,
    AcceptLoanRequestService acceptLoanRequestService)
    {
        _bookRepository = bookRepository;
        _shippingRepository = shippingRepository;
        _acceptLoanRequestService = acceptLoanRequestService;
    }

    public async Task Handle(AcceptLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException("Book not found");

        var shipping = _acceptLoanRequestService.AcceptLoanRequest(book, request.BookOwner);
        await _shippingRepository.AddAsync(shipping);
        await _bookRepository.SaveAsync();
    }
}