using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Portal.Exceptions.ExceptionHandling;

public class DefaultExceptionLogger : IExceptionLogger
{
    public DefaultExceptionLogger(HttpContext httpContext, Exception exception)
    {
        HttpContext = httpContext;
        Exception = exception;
    }

    public string ErrorMessageToLog { get; set; }

    protected HttpContext HttpContext { get; }

    protected Exception Exception { get; }

    public virtual void Log()
    {
        var logger = HttpContext.RequestServices.GetRequiredService<ILogger>();

        ErrorMessageToLog = ErrorMessageToLog ?? Exception.Message;
        var url = HttpContext.Request.GetDisplayUrl();

        logger.LogError($"Unhandled exception at {{URL}}: {ErrorMessageToLog}", Exception, url);
    }
}