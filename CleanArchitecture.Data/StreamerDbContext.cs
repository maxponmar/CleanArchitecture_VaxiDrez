namespace CleanArchitecture.Data;

public class StreamerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Streamer;User Id=sa;Password=development.2025!;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False");
    }
    
    public DbSet<Streamer> Streamers { get; set; }
    public DbSet<Video> Videos { get; set; }
}