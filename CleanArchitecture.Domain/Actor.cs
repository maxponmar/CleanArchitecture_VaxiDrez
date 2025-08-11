namespace CleanArchitecture.Domain;

public sealed class Actor : BaseDomainModel
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public ICollection<Video>? Videos { get; set; } = new HashSet<Video>();
}