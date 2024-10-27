using Bookify.Application.Apartments;
using Bookify.Application.IntegrationTests.Infrastructure;
using Bookify.Domain.Abstractions;
using FluentAssertions;

namespace Bookify.Application.IntegrationTests.Apartments;

public class SearchApartmentTests : BaseIntegrationTest
{
    public SearchApartmentTests(IntegrationTestWebAppFactory webAppFactory)
        : base(webAppFactory)
    {
    }

    [Fact]
    public async Task SearchApartments_ShouldReturnEmptyList_WhenDateRangeIsInvalid()
    {
        // Arrange
        SearchApartmentsQuery query = 
            new SearchApartmentsQuery(new DateOnly(2024, 1, 10), new DateOnly(2024, 1, 1));

        // Act
        Result<IReadOnlyList<ApartmentResponse>> result =
            await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchApartments_ShouldReturnApartments_WhenDateRangeIsValid()
    {
        // Arrange
        SearchApartmentsQuery query =
            new SearchApartmentsQuery(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Act
        Result<IReadOnlyList<ApartmentResponse>> result =
            await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
