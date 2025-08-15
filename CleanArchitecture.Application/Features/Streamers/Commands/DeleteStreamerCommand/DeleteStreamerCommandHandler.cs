namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamerCommand;

public class DeleteStreamerCommandHandler(
    IStreamerRepository streamerRepository, 
    ILogger<DeleteStreamerCommandHandler> logger)
{
    public async Task Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await streamerRepository.GetByIdAsync(request.Id);
        await streamerRepository.DeleteAsync(streamerToDelete);
        logger.LogInformation($"Streamer with id: {request.Id} deleted successfully.");
    }
}