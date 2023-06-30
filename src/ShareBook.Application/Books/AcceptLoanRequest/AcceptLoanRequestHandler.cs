using MediatR;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Application.Books;

public class AcceptLoanRequestHandler : IRequestHandler<AcceptLoanRequestCmd>
{
    private readonly IBookRepository _bookRepository;
    private readonly DomainEventDispatcher _domainEventDispatcher;

    public AcceptLoanRequestHandler(IBookRepository bookRepository, DomainEventDispatcher domainEventDispatcher)
    {
        _bookRepository = bookRepository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task Handle(AcceptLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
            throw new NotFoundException("Book not found");

        book.AcceptLoanRequest(request.UserId);

        await _bookRepository.SaveAsync();

        IEnumerable<DomainEvent> events = book.ReleaseEvents();
        await _domainEventDispatcher.DispatchEventsAsync(events);
    }
}