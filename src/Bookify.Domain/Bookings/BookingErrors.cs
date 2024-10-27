using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;
public class BookingErrors
{
    public static Error NotFound = new Error("Booking.NotFound", "The booking with the specified identifier was not found");
    public static Error Overlap = new Error("Booking.Overlap", "The current booking is overlapping with an existing one");
    public static Error NotReserved = new Error("Booking.NotReserved", "The booking is not pending");
    public static Error NotConfirmed = new Error("Booking.NotConfirmed", "The booking is not confirmed");
    public static Error AlreadyStarted = new Error("Booking.AlreadyStarted", "The booking has already started");
}
