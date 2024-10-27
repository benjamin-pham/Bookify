using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;
internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatusess =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed,
    };
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default)
    {
        return DbContext
            .Set<Booking>()
            .AnyAsync(booking => 
                booking.ApartmentId == apartment.Id
                && booking.Duration.Start <= duration.End
                && booking.Duration.End >= duration.Start
                && ActiveBookingStatusess.Contains(booking.Status), cancellationToken);
    }
}
