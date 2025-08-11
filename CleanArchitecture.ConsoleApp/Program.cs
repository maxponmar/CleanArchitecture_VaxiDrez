StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
await QueryFilter();

async Task QueryFilter()
{
    var streamers = await dbContext.Streamers
        .Where(x => x.Nombre != null && x.Nombre.Contains("Disney"))
        .ToListAsync();
    
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

void QueryStreaming()
{
    var streamers = dbContext.Streamers.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://www.disney.com/"
    };

    dbContext.Streamers.Add(streamer);
    await dbContext.SaveChangesAsync();


    var movies = new List<Video>()
    {
        new()
        {
            Nombre = "Toy Story",
            StreamerId = streamer.Id
        },
        new()
        {
            Nombre = "Bichos",
            StreamerId = streamer.Id
        },
        new()
        {
            Nombre = "Tarzan",
            StreamerId = streamer.Id
        }
    };

    await dbContext.Videos.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}