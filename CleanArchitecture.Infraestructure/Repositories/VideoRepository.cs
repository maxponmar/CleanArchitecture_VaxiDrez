namespace CleanArchitecture.Infraestructure.Repositories;

public class VideoRepository(StreamerDbContext context) : BaseRepository<Video>(context), IVideoRepository
{
    public async Task<Video?> GetVideoByName(string name)
    {
        return await context.Videos.Where(v => v.Nombre == name).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
    {
        return await context.Videos.Where(v => v.CreatedBy == username).ToListAsync();
    }
}