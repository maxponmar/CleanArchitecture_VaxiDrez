﻿namespace CleanArchitecture.Application.Features.Streamers.Commands;

public class CreateStreamerCommand
{
    public string Nombre { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}