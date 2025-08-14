namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQueryHandler(IVideoRepository videoRepository)
{
    public async Task<List<VideosVm>> Handle(GetVideosListQuery getVideosListQuery)
    {
        var videos = await videoRepository
            .GetVideoByUsername(getVideosListQuery.Username);
        
        return videos.Adapt<List<VideosVm>>();
    }
}