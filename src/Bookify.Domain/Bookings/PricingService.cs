using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;
public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        Currency currency = apartment.Price.Currency;

        Money priceForPeriod = new Money(
            apartment.Price.Amount * period.LengthInDays,
            currency);

        decimal percentageUpCharge = 0;
        foreach (Amenity amenity in apartment.Amenities)
        {
            percentageUpCharge += amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirCondtioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0
            };
        }

        Money amenitiesUpCharge = Money.Zero(currency);
        if (percentageUpCharge > 0)
        {
            amenitiesUpCharge = new Money(
                priceForPeriod.Amount * percentageUpCharge,
                currency);
        }

        Money totalPrice = Money.Zero(currency);

        totalPrice += priceForPeriod;

        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }

        totalPrice += amenitiesUpCharge;

        return new PricingDetails(PriceForPeriod: priceForPeriod,
                CleaningFee: apartment.CleaningFee,
                AmenitiesUpCharge: amenitiesUpCharge,
                TotalPrice: totalPrice);
    }
}
