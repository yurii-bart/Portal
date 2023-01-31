namespace Portal.Exceptions.ExceptionHandling;

/// <summary>Error DTO</summary>
public class ErrorDto
{
    /// <summary>The error field/property name.</summary>
    public string field { get; set; }

    /// <summary>The error resource key for localazed message</summary>
    public string message { get; set; }

    public string resourceId { get; set; }
}