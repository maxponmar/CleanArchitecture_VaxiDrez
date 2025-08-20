// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using CleanArchitecture.Application.Constants;
global using CleanArchitecture.Application.Contracts.Identity;
global using CleanArchitecture.Application.Models.Identity;
global using CleanArchitecture.Identity.Configurations;
global using CleanArchitecture.Identity.Models;
global using CleanArchitecture.Identity.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;