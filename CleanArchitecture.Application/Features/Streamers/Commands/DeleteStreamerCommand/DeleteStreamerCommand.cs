namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamerCommand;

public class DeleteStreamerCommand(int id)
{
    public int Id { get; set; } = id;
}