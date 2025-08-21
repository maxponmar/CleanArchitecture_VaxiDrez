using Wolverine;

namespace CleanArchitecture.Application.Middlewares;

public static class ValidationMiddleware
{
    public static async Task BeforeAsync<T>(T message, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();
        
        if (validator == null) return;
        
        var validationResult = await validator.ValidateAsync(message, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.Errors);
        }
    }
}