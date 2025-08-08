StreamerDbContext dbContext = new();

Streamer streamer = new()
{
    Nombre = "Amazon Prime",
    Url = "https://www.amazonprime.com/"
};

dbContext.Streamers.Add(streamer);
await dbContext.SaveChangesAsync();


var movies = new List<Video>()
{
    new()
    {
        Nombre = "The Matrix",
        StreamerId = streamer.Id
    },
    new()
    {
        Nombre = "The Matrix Reloaded",
        StreamerId = streamer.Id
    },
    new()
    {
        Nombre = "The Matrix Revolutions",
        StreamerId = streamer.Id
    }
};

await dbContext.Videos.AddRangeAsync(movies);
await dbContext.SaveChangesAsync();