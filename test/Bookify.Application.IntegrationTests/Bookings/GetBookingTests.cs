﻿using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.IntegrationTests.Infrastructure;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using FluentAssertions;

namespace Bookify.Application.IntegrationTests.Bookings;
public class GetBookingTests : BaseIntegrationTest
{
    private static readonly Guid BookingId = Guid.NewGuid();

    public GetBookingTests(IntegrationTestWebAppFactory webAppFactory)
        : base(webAppFactory)
    {
    }

    [Fact]
    public async Task GetBooking_ShouldReturnFailure_WhenBookingIsNotFound()
    {
        // Arrange
        GetBookingQuery command = new GetBookingQuery(BookingId);

        // Act
        Result<BookingResponse> result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(BookingErrors.NotFound);
    }
}

