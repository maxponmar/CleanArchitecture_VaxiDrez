namespace CleanArchitecture.Application.Features.Streamers.Commands;

public class StreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
{
    public StreamerCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("{Nombre} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{Nombre} must not exceed 50 characters.}");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("{Url} is required.")
            .NotNull();
    }
}