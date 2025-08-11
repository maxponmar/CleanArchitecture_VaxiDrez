namespace CleanArchitecture.Domain;
public class Video : BaseDomainModel
{
    public string? Nombre { get; set; }
    public int? StreamerId { get; set; }
    public Streamer? Streamer { get; set; }
    public virtual ICollection<Actor>? Actores { get; set; } = new HashSet<Actor>();
    public virtual ICollection<Director>? Directores { get; set; }
}
