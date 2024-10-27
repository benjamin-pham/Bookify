using Bookify.Application.Users.GetLoggedInUser;
using Bookify.Application.Users.LogInUser;
using Bookify.Application.Users.RegisterUser;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Bookify.Api.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand rquest, CancellationToken cancellationToken)
    {
        Result<Guid> result = await _sender.Send(rquest, cancellationToken);

        return Ok(result.Value);
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogInUserCommand request, CancellationToken cancellationToken)
    {
        Result<AccessTokenRespsonse> result = await _sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("me")]
    //[Authorize(Roles = Roles.Registered)]
    [HasPermission(Permissions.UsersRead)]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> GetLoggedInUserV1(CancellationToken cancellationToken)
    {
        GetLoggedInUserQuery query = new GetLoggedInUserQuery();

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("me")]
    //[Authorize(Roles = Roles.Registered)]
    [HasPermission(Permissions.UsersRead)]
    [MapToApiVersion(ApiVersions.V2)]
    public async Task<IActionResult> GetLoggedInUserV2(CancellationToken cancellationToken)
    {
        GetLoggedInUserQuery query = new GetLoggedInUserQuery();

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
