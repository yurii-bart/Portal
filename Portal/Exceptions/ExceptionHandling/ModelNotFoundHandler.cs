using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Portal.Exceptions.ExceptionHandling;

public class ModelNotFoundHandler : DefaultExceptionHandler
{
    public ModelNotFoundHandler(HttpContext httpContext, Exception exception) : base(httpContext, exception)
    {
    }

    protected override int GetStatusCode()
    {
        return (int) HttpStatusCode.NotFound;
    }
}