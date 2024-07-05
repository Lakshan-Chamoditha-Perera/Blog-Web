namespace BlogApp.Payloads;

/**
 * Standard response - used for API responses
 *
 * Success - bool
 * Message - string
 * Data - T
 */
public class StandardResponse<T>
{
    public StandardResponse(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}