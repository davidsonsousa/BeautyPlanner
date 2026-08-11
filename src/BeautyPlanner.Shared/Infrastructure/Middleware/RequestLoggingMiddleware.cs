namespace BeautyPlanner.Shared.Infrastructure.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger("Request");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.TraceIdentifier;
        var path = context.Request.Path;
        var method = context.Request.Method;

        var start = Stopwatch.GetTimestamp();

        using (_logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId, ["RequestPath"] = path, ["HttpMethod"] = method }))
        {
            _logger.LogInformation(">>> Request started {CorrelationId} {Path} {Method}", correlationId, path, method);

            try
            {
                await _next(context);
            }
            finally
            {
                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                var statusCode = context.Response.StatusCode;

                _logger.LogInformation("<<< Request finished {StatusCode} in {Elapsed}ms", statusCode, elapsedMs);
            }
        }
    }

    private static double GetElapsedMilliseconds(long start, long stop)
    {
        return (stop - start) * 1000 / (double)Stopwatch.Frequency;
    }
}
