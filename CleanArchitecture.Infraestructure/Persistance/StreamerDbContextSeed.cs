namespace CleanArchitecture.Infraestructure.Persistance;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
    {
        if (!context.Streamers.Any())
        {
            await context.Streamers.AddRangeAsync(GetPreconfiguredStreamers());
            await context.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(StreamerDbContext).Name);
        }
    }

    private static IEnumerable<Streamer> GetPreconfiguredStreamers()
    {
        return new List<Streamer>()
        {
            new Streamer { CreatedBy = "maxponce", Nombre = "Twitch", Url = "https://www.twitch.tv" },
            new Streamer { CreatedBy = "maxponce", Nombre = "Netflix", Url = "https://www.netflix.com/" },
            new Streamer { CreatedBy = "maxponce", Nombre = "Amazon Prime", Url = "https://www.amazonprime.com/" },
        };
    }
}