using System.Collections.Generic;

namespace Portal.Exceptions.ExceptionHandling;

/// <summary>The errors container</summary>
public class ErrorsContainerDto
{
    /// <summary>The _items</summary>
    private readonly List<ErrorDto> _items = new();

    /// <summary>Gets or sets the errors.</summary>
    public List<ErrorDto> items
    {
        get => _items;
        set
        {
            _items.Clear();
            _items.AddRange(value ?? new List<ErrorDto>());
        }
    }

    public static ErrorsContainerDto CreateGlobal(string message, string resourceId = default)
    {
        return new ErrorsContainerDto
        {
            items = new List<ErrorDto>
            {
                new() {field = "global", message = message, resourceId = resourceId}
            }
        };
    }

    public static ErrorsContainerDto Failed()
    {
        return CreateGlobal("Failed");
    }
}