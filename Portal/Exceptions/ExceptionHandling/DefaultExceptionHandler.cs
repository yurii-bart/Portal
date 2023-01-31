using System.Net;
using Newtonsoft.Json.Linq;

namespace Portal.Exceptions.ExceptionHandling;

public class DefaultExceptionHandler : IExceptionHandler
{
    public DefaultExceptionHandler(HttpContext httpContext, Exception exception)
    {
        HttpContext = httpContext;
        Exception = exception;
    }

    protected HttpContext HttpContext { get; }

    protected Exception Exception { get; }

    public virtual void Handle()
    {
        var errorContainer = CreateErrorResponse();
        var jsonResponse = JObject.FromObject(errorContainer);
        WriteHttpResponse((HttpStatusCode) GetStatusCode(), jsonResponse);
    }

    protected virtual int GetStatusCode()
    {
        return (int) HttpStatusCode.InternalServerError;
    }

    protected virtual ErrorsContainerDto CreateErrorResponse()
    {
        var message = Exception.Message;
        return ErrorsContainerDto.CreateGlobal(message);
    }

    protected void WriteHttpResponse(HttpStatusCode statusCode, JObject jsonResponse)
    {
        var httpResponse = HttpContext.Response;
        httpResponse.Clear();
        httpResponse.StatusCode = (int) statusCode;
        httpResponse.ContentType = "application/json";
        Task.WaitAll(httpResponse.WriteAsync(jsonResponse.ToString()));
    }
}