using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace GlobalErrorHandlingDemo;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        logger.LogError(exception,
            $"Could not process a request on machine {Environment.MachineName}. TraceId: {traceId}");

        await GenerateProblemDetails(httpContext, traceId, exception);

        return true;
    }

    private static async Task GenerateProblemDetails(HttpContext httpContext,
        string traceId,
        Exception exception)
    {
        var (statusCode, title) = MapExceptionWithStatusCode(exception);

        await Results.Problem(title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                {
                    "traceId", traceId
                }
            }).ExecuteAsync(httpContext);
    }

    private static (int statusCode, string title) MapExceptionWithStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "We are sorry for the inconvenience but we are on it.")
        };
    }
}