using CleanArchitecture.Application.Exceptions;
using Wolverine.Persistence;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamerCommand;

public class UpdateStreamerCommandHandler(
    IStreamerRepository streamerRepository,
    ILogger<UpdateStreamerCommandHandler> logger)
{
    public async Task<UpdateStreamerCommandDto> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToUpdate = await streamerRepository.GetByIdAsync(request.Id);
        
        // TODO: Move this expection to the repository
        // if (streamer == null)
        // {
        //     logger.LogError($"Streamer with id: {request.Id}, Not Found.");
        //     throw new NotFoundException(nameof(Streamer), request.Id);
        // }
        
        request.Adapt(streamerToUpdate);
        await streamerRepository.UpdateAsync(streamerToUpdate);
        logger.LogInformation($"Streamer with id: {request.Id} updated successfully.");
        
        return streamerToUpdate.Adapt<UpdateStreamerCommandDto>();
    }
}