namespace CleanArchitecture.Application.Features.Streamers.Commands;

public class CreateStreamerCommandHandler(
    IStreamerRepository streamerRepository, 
    IEmailService emailService, 
    ILogger<CreateStreamerCommandHandler> logger)
{
    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
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