namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQuery(string username)
{
    public string Username { get; set; } = username;
}