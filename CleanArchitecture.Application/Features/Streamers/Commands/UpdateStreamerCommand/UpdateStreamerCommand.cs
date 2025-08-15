namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamerCommand;

public class UpdateStreamerCommand
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}