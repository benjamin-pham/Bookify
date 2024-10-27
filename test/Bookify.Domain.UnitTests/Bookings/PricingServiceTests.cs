using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Shared;
using Bookify.Domain.UnitTests.Apartments;
using FluentAssertions;

namespace Bookify.Domain.UnitTests.Bookings;
public class PricingServiceTests
{
    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice()
    {
        // Arrange
        Money price = new Money(10.0m, Currency.Usd);
        DateRange period = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        Money expectedTotalPrice = new Money(price.Amount * period.LengthInDays, price.Currency);
        Apartment apartment = ApartmentData.Create(price);
        PricingService pricingService = new PricingService();

        // Act
        PricingDetails pricingDetails = pricingService.CalculatePrice(apartment, period);

        // Assert
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }

    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice_WhenCleaningFeeIncluded()
    {
        // Arrange
        Money price = new Money(10.0m, Currency.Usd);
        Money cleaningFee = new Money(99.99m, Currency.Usd);
        DateRange period = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        Money expectedTotalPrice = new Money(price.Amount * period.LengthInDays + cleaningFee.Amount, price.Currency);
        Apartment apartment = ApartmentData.Create(price, cleaningFee);
        PricingService pricingService = new PricingService();

        // Act
        PricingDetails pricingDetails = pricingService.CalculatePrice(apartment, period);

        // Assert
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }
}
