using MediatR;

namespace ShareBook.Domain.Shared.Primitives;

public abstract record DomainEvent : INotification
{
    public DateTime DateOcurred { get; init; } = DateTime.UtcNow;
}