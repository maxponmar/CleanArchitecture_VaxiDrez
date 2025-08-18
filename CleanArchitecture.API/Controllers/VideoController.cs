namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class VideoController(IMessageBus messageBus) : ControllerBase
{
    [HttpGet("{username}", Name = "GetVideosByUsername")]
    [ProducesResponseType(typeof(IEnumerable<VideosVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
    {
        var getUserVideosQuery = new GetVideosListQuery(username);
        var videos = await messageBus.InvokeAsync<IEnumerable<VideosVm>>(getUserVideosQuery);
    
        return Ok(videos);
    }
}