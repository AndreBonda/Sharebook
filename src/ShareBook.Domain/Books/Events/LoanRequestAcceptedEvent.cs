using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.Domain.Books.Events;

public record LoanRequestAcceptedEvent : DomainEvent
{
    public required Guid BookId { get; init; }
}