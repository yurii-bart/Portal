using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Portal.Exceptions.ExceptionHandling;

public static class ExceptionLoggerFactory
{
    public static IExceptionLogger GetLogger(ExceptionContext context)
    {
        return GetLogger(context.HttpContext, context.Exception);
    }

    public static IExceptionLogger GetLogger(HttpContext httpContext, Exception exception)
    {
        var exceptionType = exception.GetType();
        switch (exceptionType.Name)
        {
            default:
                return new DefaultExceptionLogger(httpContext, exception);
        }
    }
}