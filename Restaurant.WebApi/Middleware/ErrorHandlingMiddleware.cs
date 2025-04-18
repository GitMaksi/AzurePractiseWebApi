using Restaurant.Doman.Exceptions;

namespace Restaurant.WebApi.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundException)
        {
            logger.LogError(notFoundException, $"Exception thrown with message: \n {notFoundException.Message}");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Exception thrown with message: \n {e.Message}");
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}