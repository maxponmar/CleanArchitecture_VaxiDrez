namespace CleanArchitecture.API.Exceptions;

public class CodeErrorResponse(int statusCode, string? message)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message ?? string.Empty;

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unknown Error"
        };
    }
}