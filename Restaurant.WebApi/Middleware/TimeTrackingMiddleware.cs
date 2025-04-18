namespace Restaurant.WebApi.Middleware;

public class TimeTrackingMiddleware(ILogger<TimeTrackingMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogInformation("Tracking time middleware started");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await next.Invoke(context);
        watch.Stop();
        logger.LogInformation($"Tracked time of request exectution: {watch.Elapsed}");
    }
}