namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StreamerController(IMessageBus messageBus) : ControllerBase
{
    [HttpPost(Name = "CreateStreamer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand command)
    {
        return await messageBus.InvokeAsync<int>(command);
    }

    [HttpPut(Name = "UpdateStreamer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand command)
    {
        await messageBus.InvokeAsync(command);
        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeleteStreamer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteStreamer(int id)
    {
        var command = new DeleteStreamerCommand(id);
        await messageBus.InvokeAsync(command);
        return NoContent();
    }
}