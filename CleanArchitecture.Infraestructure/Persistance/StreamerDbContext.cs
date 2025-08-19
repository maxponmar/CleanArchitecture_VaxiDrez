namespace CleanArchitecture.Infraestructure.Persistance;

public class StreamerDbContext(DbContextOptions<StreamerDbContext> options) : 
    DbContext(options)
{
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //             "Server=localhost,1433;Database=Streamer;User Id=sa;Password=development.2025!;" +
    //             "Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False")
    //         .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information)
    //         .EnableSensitiveDataLogging();
    // }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case(EntityState.Added):
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = "system";
                    break;
                
                case(EntityState.Modified):
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = "system";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
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