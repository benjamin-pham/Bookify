using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingConfirmDomainEvent(Guid BookingId) : IDomainEvent;