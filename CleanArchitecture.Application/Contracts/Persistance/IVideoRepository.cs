namespace CleanArchitecture.Application.Contracts.Persistance;

public interface IVideoRepository : IAsyncRepository<Video>
{
    Task<Video?> GetVideoByName(string name);
    Task<IEnumerable<Video>> GetVideoByUsername(string username);
}