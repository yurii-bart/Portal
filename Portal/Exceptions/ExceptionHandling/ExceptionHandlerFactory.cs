using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Portal.Exceptions.ExceptionHandling;

public static class ExceptionHandlerFactory
{
    public static IExceptionHandler GetHandler(ExceptionContext context)
    {
        return GetHandler(context.HttpContext, context.Exception);
    }

    public static IExceptionHandler GetHandler(HttpContext httpContext, Exception exception)
    {
        if(exception is ModelNotFoundException)
        {
            return new ModelNotFoundHandler(httpContext, exception);
        }

        return new DefaultExceptionHandler(httpContext, exception);
    }
}