using Microsoft.AspNetCore.Diagnostics;

namespace SurvayBucketsApi.Errors;

public class GlobalExceptionHandeling(ILogger<GlobalExceptionHandeling> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandeling> _logger = logger;


    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {

        _logger.LogError(exception, "An unhandled exception occurred while processing the request. {mesage}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred!",
            Detail = "Please try again later or contact support if the problem persists.",
            Type = "https://httpstatuses.com/500"
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
