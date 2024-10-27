using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shared;
using Bookify.Domain.UnitTests.Apartments;
using Bookify.Domain.UnitTests.Infrastructure;
using Bookify.Domain.UnitTests.Users;
using Bookify.Domain.Users;
using FluentAssertions;

namespace Bookify.Domain.UnitTests.Bookings;
public class BookingTests : BaseTest
{
    [Fact]
    public void Reserve_Should_RaiseBookingReservedDomainEvent()
    {
        // Arrange
        User user = UserData.Create();
        Money price = new Money(10.0m, Currency.Usd);
        DateRange period = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        Apartment apartment = ApartmentData.Create(price);
        PricingService pricingService = new PricingService();

        // Act
        Booking booking = Booking.Reserve(apartment, user.Id, period, DateTime.UtcNow, pricingService);

        // Assert
        BookingReservedDomainEvent domainEvent = AssertDomainEventWasPublished<BookingReservedDomainEvent>(booking);

        domainEvent.BookingId.Should().Be(booking.Id);
    }
}
