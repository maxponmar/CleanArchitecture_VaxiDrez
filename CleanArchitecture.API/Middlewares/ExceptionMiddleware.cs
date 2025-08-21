using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CleanArchitecture.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;

            var result = string.Empty;
            switch (exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                
                case ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var validationJson = JsonSerializer.Serialize(validationException.Errors);
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, exception.Message,validationJson));
                    break;
                
                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }
            
            if(string.IsNullOrEmpty(result))
                result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, exception.Message, exception.StackTrace ?? string.Empty));

            
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}