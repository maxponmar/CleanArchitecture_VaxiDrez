namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamerCommand;

public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotNull().WithMessage("{Nombre} is required");
        
        RuleFor(x => x.Url)
            .NotNull().WithMessage("{Url} is required");
    }
}