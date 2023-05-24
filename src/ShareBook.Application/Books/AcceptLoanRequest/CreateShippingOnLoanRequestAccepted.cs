using MediatR;
using ShareBook.Domain.Books.Events;
using ShareBook.Domain.Shippings;

namespace ShareBook.Application.Books;

public class CreateShippingOnLoanRequestAccepted : INotificationHandler<LoanRequestAcceptedEvent>
{
    private readonly IShippingRepository _shippingRepository;

    public CreateShippingOnLoanRequestAccepted(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task Handle(LoanRequestAcceptedEvent notification, CancellationToken cancellationToken)
    {
        var shippingId = Guid.NewGuid();
        var shipping = Shipping.New(shippingId, notification.BookId);
        await _shippingRepository.AddAsync(shipping);
        await _shippingRepository.SaveAsync();
    }
}