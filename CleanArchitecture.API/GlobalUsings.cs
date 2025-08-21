// Global using directives

global using System.Net;
global using System.Text.Json;
global using CleanArchitecture.API.Exceptions;
global using CleanArchitecture.API.Middlewares;
global using CleanArchitecture.Application;
global using CleanArchitecture.Application.Contracts.Identity;
global using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamerCommand;
global using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamerCommand;
global using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamerCommand;
global using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
global using CleanArchitecture.Application.Models.Identity;
global using CleanArchitecture.Identity;
global using CleanArchitecture.Infraestructure;
global using CleanArchitecture.Infraestructure.Persistance;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Wolverine;