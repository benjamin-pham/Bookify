using Bookify.Application.Exceptions;
using Bookify.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookify.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            ExceptionDetails exceptionDetails = GetExceptionDetails(exception);

            ProblemDetails propblemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
                Type = exceptionDetails.Type
            };

            if (exceptionDetails.Errors is not null)
            {
                propblemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(propblemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch{
            ValidationException validationException => new ExceptionDetails(
                Status: StatusCodes.Status400BadRequest,
                Type: "ValidationFailure",
                Title: "Validation error",
                Detail: "One or more validation errors has occured",
                Errors: validationException.Errors),
            _ => new ExceptionDetails(
                Status: StatusCodes.Status500InternalServerError,
                Type: "ServerError",
                Title: "Server error",
                Detail: "An unexpected error has occured",
                Errors: null)
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}
