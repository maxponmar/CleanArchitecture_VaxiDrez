namespace CleanArchitecture.Application.Middlewares;

public class UnhandledExceptionMiddleware(ILogger<UnhandledExceptionMiddleware> logger)
{
    private readonly ILogger _logger = logger;

    public async ValueTask<object?> HandleAsync(object message, Func<object, ValueTask<object?>> inner)
    {
        try
        {
            return await inner(message);
        }
        catch (Exception ex)
        {
            var messageName = message.GetType().Name;
            
            _logger.LogError(ex, 
                "Unhandled Exception: An exception occurred while processing request {MessageName}. Message details: {@Message}", 
                messageName, 
                message);

            throw;
        }
    }
}