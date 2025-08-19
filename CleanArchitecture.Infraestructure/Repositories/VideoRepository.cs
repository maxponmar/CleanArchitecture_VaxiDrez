namespace CleanArchitecture.Infraestructure.Repositories;

public class VideoRepository(StreamerDbContext context) : BaseRepository<Video>(context), IVideoRepository
{
    private readonly StreamerDbContext _context = context;

    public async Task<Video?> GetVideoByName(string name)
    {
        return await _context.Videos.Where(v => v.Nombre == name).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
    {
        return await _context.Videos.Where(v => v.CreatedBy == username).ToListAsync();
    }
}