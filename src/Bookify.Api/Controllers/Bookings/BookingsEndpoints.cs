using Asp.Versioning;
using Asp.Versioning.Builder;
using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings;

public static class BookingsEndpoints
{
    public static IEndpointRouteBuilder MapBookingEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("bookings/{id}", GetBooking)
            .RequireAuthorization()
            .WithName(nameof(GetBooking));

        builder.MapPost("bookings", ReserveBooking)
            .RequireAuthorization();

        return builder;
    }

    public static async Task<IResult> GetBooking(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        Result<BookingResponse> result = await sender.Send(new GetBookingQuery(id), cancellationToken);

        if (result.IsFailure)
            return Results.NotFound();

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> ReserveBooking([FromBody] ReserveBookingCommand request, ISender sender, CancellationToken cancellationToken)
    {
        Result<Guid> result = await sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.CreatedAtRoute(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}