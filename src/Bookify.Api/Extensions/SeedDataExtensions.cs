using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartments;
using Dapper;
using System.Data;

namespace Bookify.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        Faker faker = new Faker();

        List<object> apartments = new();
        for (int i = 0; i < 100; i ++)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Company.CompanyName(),
                Description = "Amazing view",
                AddressCountry = faker.Address.Country(),
                AddressState = faker.Address.State(),
                AddressZipCode = faker.Address.ZipCode(),
                AddressCity = faker.Address.City(),
                AddressStreet = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(50, 1000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(50, 1000),
                CleaningFeeCurrency = "USD",
                //Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                LastBookingOnUtc = DateTime.MinValue
            });
        }

        const string sql = """
        INSERT INTO apartments
        (id, "name", description, address_country, address_state, address_zip_code, address_city, address_street, price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, last_booked_on_utc)
        VALUES
        (@Id, @Name, @Description, @AddressCountry, @AddressState, @AddressZipCode, @AddressCity, @AddressStreet, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleaningFeeCurrency, @LastBookingOnUtc)
        """;

        connection.Execute(sql, apartments);
    }
}
