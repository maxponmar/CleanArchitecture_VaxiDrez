namespace CleanArchitecture.Domain;
public sealed class Video : BaseDomainModel
{
    public string? Nombre { get; set; }
    public int? StreamerId { get; set; }
    public Streamer? Streamer { get; set; }
    public ICollection<Actor>? Actores { get; set; } = new HashSet<Actor>();
}
