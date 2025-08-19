namespace CleanArchitecture.Infraestructure.Persistance;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
    {
        try
        {
            if (!context.Streamers.Any())
            {
                await context.Streamers.AddRangeAsync(GetPreconfiguredStreamers());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}",
                    typeof(StreamerDbContext).Name);
            }

            if (!context.Actors.Any())
            {
                await context.Actors.AddRangeAsync(GetPreconfiguredActors());
                await context.SaveChangesAsync();
                logger.LogInformation("Actors seeded for context {DbContextName}", typeof(StreamerDbContext).Name);
            }

            if (!context.Videos.Any())
            {
                await context.Videos.AddRangeAsync(GetPreconfiguredVideos());
                await context.SaveChangesAsync();
                logger.LogInformation("Videos seeded for context {DbContextName}", typeof(StreamerDbContext).Name);
            }

            if (!context.Directors.Any())
            {
                var directors = await GetPreconfiguredDirectors(context);
                await context.Directors.AddRangeAsync(directors);
                await context.SaveChangesAsync();
                logger.LogInformation("Directors seeded for context {DbContextName}", typeof(StreamerDbContext).Name);
            }

            if (!context.VideoActors.Any())
            {
                var videoActors = await GetPreconfiguredVideoActors(context);
                await context.VideoActors.AddRangeAsync(videoActors);
                await context.SaveChangesAsync();
                logger.LogInformation("VideoActors seeded for context {DbContextName}", typeof(StreamerDbContext).Name);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred seeding the DB.");
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

    private static IEnumerable<Actor> GetPreconfiguredActors()
    {
        return new List<Actor>()
        {
            new Actor { CreatedBy = "maxponce", Nombre = "Robert", Apellido = "Downey Jr." },
            new Actor { CreatedBy = "maxponce", Nombre = "Scarlett", Apellido = "Johansson" },
            new Actor { CreatedBy = "maxponce", Nombre = "Chris", Apellido = "Evans" },
            new Actor { CreatedBy = "maxponce", Nombre = "Jennifer", Apellido = "Lawrence" },
            new Actor { CreatedBy = "maxponce", Nombre = "Leonardo", Apellido = "DiCaprio" },
            new Actor { CreatedBy = "maxponce", Nombre = "Emma", Apellido = "Stone" },
            new Actor { CreatedBy = "maxponce", Nombre = "Ryan", Apellido = "Gosling" },
            new Actor { CreatedBy = "maxponce", Nombre = "Margot", Apellido = "Robbie" }
        };
    }

    private static IEnumerable<Video> GetPreconfiguredVideos()
    {
        return new List<Video>()
        {
            new Video { CreatedBy = "maxponce", Nombre = "Iron Man", StreamerId = 1 },
            new Video { CreatedBy = "maxponce", Nombre = "The Avengers", StreamerId = 2 },
            new Video { CreatedBy = "maxponce", Nombre = "Captain America: The First Avenger", StreamerId = 2 },
            new Video { CreatedBy = "maxponce", Nombre = "The Hunger Games", StreamerId = 3 },
            new Video { CreatedBy = "maxponce", Nombre = "Inception", StreamerId = 2 },
            new Video { CreatedBy = "maxponce", Nombre = "La La Land", StreamerId = 2 },
            new Video { CreatedBy = "maxponce", Nombre = "The Wolf of Wall Street", StreamerId = 3 },
            new Video { CreatedBy = "maxponce", Nombre = "Barbie", StreamerId = 2 }
        };
    }

    private static async Task<IEnumerable<Director>> GetPreconfiguredDirectors(StreamerDbContext context)
    {
        var videos = await context.Videos.ToListAsync();

        return new List<Director>()
        {
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Jon", Apellido = "Favreau",
                VideoId = videos.First(v => v.Nombre == "Iron Man").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Joss", Apellido = "Whedon",
                VideoId = videos.First(v => v.Nombre == "The Avengers").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Joe", Apellido = "Johnston",
                VideoId = videos.First(v => v.Nombre == "Captain America: The First Avenger").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Gary", Apellido = "Ross",
                VideoId = videos.First(v => v.Nombre == "The Hunger Games").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Christopher", Apellido = "Nolan",
                VideoId = videos.First(v => v.Nombre == "Inception").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Damien", Apellido = "Chazelle",
                VideoId = videos.First(v => v.Nombre == "La La Land").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Martin", Apellido = "Scorsese",
                VideoId = videos.First(v => v.Nombre == "The Wolf of Wall Street").Id
            },
            new Director
            {
                CreatedBy = "maxponce", Nombre = "Greta", Apellido = "Gerwig",
                VideoId = videos.First(v => v.Nombre == "Barbie").Id
            }
        };
    }

    private static async Task<IEnumerable<VideoActor>> GetPreconfiguredVideoActors(StreamerDbContext context)
    {
        var videos = await context.Videos.ToListAsync();
        var actors = await context.Actors.ToListAsync();

        return new List<VideoActor>()
        {
            // Iron Man - Robert Downey Jr.
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "Iron Man").Id,
                ActorId = actors.First(a => a.Nombre == "Robert" && a.Apellido == "Downey Jr.").Id
            },

            // The Avengers - Multiple actors
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "The Avengers").Id,
                ActorId = actors.First(a => a.Nombre == "Robert" && a.Apellido == "Downey Jr.").Id
            },
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "The Avengers").Id,
                ActorId = actors.First(a => a.Nombre == "Scarlett" && a.Apellido == "Johansson").Id
            },
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "The Avengers").Id,
                ActorId = actors.First(a => a.Nombre == "Chris" && a.Apellido == "Evans").Id
            },

            // Captain America - Chris Evans
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "Captain America: The First Avenger").Id,
                ActorId = actors.First(a => a.Nombre == "Chris" && a.Apellido == "Evans").Id
            },

            // The Hunger Games - Jennifer Lawrence
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "The Hunger Games").Id,
                ActorId = actors.First(a => a.Nombre == "Jennifer" && a.Apellido == "Lawrence").Id
            },

            // Inception - Leonardo DiCaprio
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "Inception").Id,
                ActorId = actors.First(a => a.Nombre == "Leonardo" && a.Apellido == "DiCaprio").Id
            },

            // La La Land - Emma Stone & Ryan Gosling
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "La La Land").Id,
                ActorId = actors.First(a => a.Nombre == "Emma" && a.Apellido == "Stone").Id
            },
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "La La Land").Id,
                ActorId = actors.First(a => a.Nombre == "Ryan" && a.Apellido == "Gosling").Id
            },

            // The Wolf of Wall Street - Leonardo DiCaprio
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "The Wolf of Wall Street").Id,
                ActorId = actors.First(a => a.Nombre == "Leonardo" && a.Apellido == "DiCaprio").Id
            },

            // Barbie - Margot Robbie & Ryan Gosling
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "Barbie").Id,
                ActorId = actors.First(a => a.Nombre == "Margot" && a.Apellido == "Robbie").Id
            },
            new VideoActor
            {
                CreatedBy = "maxponce",
                VideoId = videos.First(v => v.Nombre == "Barbie").Id,
                ActorId = actors.First(a => a.Nombre == "Ryan" && a.Apellido == "Gosling").Id
            }
        };
    }
}