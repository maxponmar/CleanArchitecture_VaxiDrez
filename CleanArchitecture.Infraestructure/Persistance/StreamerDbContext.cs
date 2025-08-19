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
            .UsingEntity<VideoActor>(
                va => va.HasOne<Actor>()
                    .WithMany()
                    .HasForeignKey(va => va.ActorId)
                    .OnDelete(DeleteBehavior.Cascade),
                va => va.HasOne<Video>()
                    .WithMany()
                    .HasForeignKey(va => va.VideoId)
                    .OnDelete(DeleteBehavior.Cascade),
                va => va.HasKey(e => new { e.VideoId, e.ActorId })
            );

        modelBuilder.Entity<Video>()
            .HasMany(v => v.Directores)
            .WithOne(d => d.Video)
            .HasForeignKey(d => d.VideoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Additional configurations for better database design
        modelBuilder.Entity<Actor>()
            .HasIndex(a => new { a.Nombre, a.Apellido })
            .HasDatabaseName("IX_Actor_Nombre_Apellido");

        modelBuilder.Entity<Director>()
            .HasIndex(d => new { d.Nombre, d.Apellido })
            .HasDatabaseName("IX_Director_Nombre_Apellido");

        modelBuilder.Entity<Video>()
            .HasIndex(v => v.Nombre)
            .HasDatabaseName("IX_Video_Nombre");

        modelBuilder.Entity<Streamer>()
            .HasIndex(s => s.Nombre)
            .IsUnique()
            .HasDatabaseName("IX_Streamer_Nombre_Unique");

    }

    public DbSet<Streamer> Streamers { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<VideoActor> VideoActors { get; set; }
}