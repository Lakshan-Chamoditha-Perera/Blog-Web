namespace BlogApp.Payloads;
public class StandardResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public StandardResponse(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}
