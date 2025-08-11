namespace CleanArchitecture.Data;

public class StreamerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=Streamer;User Id=sa;Password=development.2025!;" +
                "Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False")
            .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(s => s.Videos)
            .WithOne(s => s.Streamer)
            .HasForeignKey(v => v.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(v => v.Actores)
            .WithMany(a => a.Videos)
            .UsingEntity<VideoActor>(va =>
                va.HasKey(e => new { e.VideoId, e.ActorId }));
    }

    public DbSet<Streamer> Streamers { get; set; }
    public DbSet<Video> Videos { get; set; }
}