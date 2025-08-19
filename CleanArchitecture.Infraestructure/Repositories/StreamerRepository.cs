namespace CleanArchitecture.Infraestructure.Repositories;

public class StreamerRepository(StreamerDbContext context) : BaseRepository<Streamer>(context), IStreamerRepository
{
    
}