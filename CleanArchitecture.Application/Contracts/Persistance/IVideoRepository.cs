namespace CleanArchitecture.Application.Contracts.Persistance;

public interface IVideoRepository : IAsyncRepository<Video>
{
    Task<IEnumerable<Video>> GetVideoByName(string name);
}