namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamerCommand;

public class CreateStreamerCommandHandler(
    IStreamerRepository streamerRepository, 
    IEmailService emailService, 
    ILogger<CreateStreamerCommandHandler> logger,
    IValidator<CreateStreamerCommand> validator)
{
    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.Errors);
        }
        
        var streamer = request.Adapt<Streamer>();
        await streamerRepository.AddAsync(streamer);
        
        logger.LogInformation($"Streamer with id {streamer.Id} created successfully");
        await SendEmailAsync(streamer);
        
        return streamer.Id;
    }
    
    private async Task SendEmailAsync(Streamer streamer)
    {
        var email = new Email()
        {
            To = "maxponce.marquez@outlook.com",
            Subject = "New Streamer Added",
            Body = $"New Streamer Added: {streamer.Nombre}"
        };

        try
        {
            await emailService.SendEmailAsync(email);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error sending email for streamer with id {streamer.Id}");
        }
    }
}