namespace CleanArchitecture.Application.Middlewares;

public class ValidationMiddleware(IServiceProvider serviceProvider)
{
    public async ValueTask<object?> HandleAsync(object message, Func<object, ValueTask<object?>> inner)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(message.GetType());

        if (serviceProvider.GetService(validatorType) is not IValidator validator) return await inner(message);
        
        var contextType = typeof(ValidationContext<>).MakeGenericType(message.GetType());
        var validationContext = Activator.CreateInstance(contextType, message);

        var validateMethod = validator.GetType().GetMethod("Validate", [contextType]);

        if (validateMethod?.Invoke(validator, [validationContext]) is ValidationResult { IsValid: false } result)
            throw new ValidationException(result.Errors);

        return await inner(message);
    }
}