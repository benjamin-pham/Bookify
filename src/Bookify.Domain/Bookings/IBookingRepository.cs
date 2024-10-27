using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;
public interface IBookingRepository
{
    void Add(Booking booking);
    Task<Booking> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default);
}
