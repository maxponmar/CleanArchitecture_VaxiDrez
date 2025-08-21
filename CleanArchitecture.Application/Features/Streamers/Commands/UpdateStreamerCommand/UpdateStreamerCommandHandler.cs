namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamerCommand;

public class UpdateStreamerCommandHandler(
    IStreamerRepository streamerRepository,
    ILogger<UpdateStreamerCommandHandler> logger,
    IValidator<UpdateStreamerCommand> validator)
{
    public async Task<UpdateStreamerCommandDto> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.Errors);
        }
        
        var streamerToUpdate = await streamerRepository.GetByIdAsync(request.Id);
        
        // TODO: Move this expection to the repository
        // if (streamer == null)
        // {
        //     logger.LogError($"Streamer with id: {request.Id}, Not Found.");
        //     throw new NotFoundException(nameof(Streamer), request.Id);
        // }
        
        request.Adapt(streamerToUpdate);
        if (streamerToUpdate == null)
            throw new NotFoundException(nameof(Streamer), request.Id);
        
        await streamerRepository.UpdateAsync(streamerToUpdate);
        
        logger.LogInformation($"Streamer with id: {request.Id} updated successfully.");
        return streamerToUpdate.Adapt<UpdateStreamerCommandDto>();
    }
}