using Asp.Versioning;
using Bookify.Application.Apartments;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/apartments")]
public class ApartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public ApartmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchApartments([FromQuery] SearchApartmentsQuery query,
        CancellationToken cancellationToken)
    {
        //SearchApartmentsQuery query = new SearchApartmentsQuery(start, end);
        Result<IReadOnlyList<ApartmentResponse>> result = await _sender.Send(query, cancellationToken);
                
        return Ok(result.Value);
    }
}
