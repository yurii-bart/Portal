namespace Portal.Exceptions.ExceptionHandling;

/// <summary>
///     Central error/exception handler Middleware
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _request;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionHandlerMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next.</param>
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _request = next;
    }

    /// <summary>
    ///     Invokes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _request(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var handler = ExceptionHandlerFactory.GetHandler(context, exception);
        handler.Handle();
        return Task.CompletedTask;
    }
}