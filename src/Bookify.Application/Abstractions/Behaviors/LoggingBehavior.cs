using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bookify.Application.Abstractions.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string name = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing request {Request}", name);

            TResponse result = await next();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Request {Request} processed successfuly", name);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError("Request {Request} processed with error", name);
                }
            }

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Request {Request} processing failed", name);

            throw;
        }
    }
}
