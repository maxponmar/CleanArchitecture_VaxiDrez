var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddWolverine(opts =>
{
    // Automatically discover and register all handlers in the current assembly
    opts.Discovery.DisableConventionalDiscovery(false);
            
    // Include assemblies for handler discovery
    opts.Discovery.IncludeAssembly(typeof(ApplicationServicesRegistration).Assembly);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<StreamerDbContextSeed>();
    
    try
    {
        var context = services.GetRequiredService<StreamerDbContext>();
        await StreamerDbContextSeed.SeedAsync(context, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the DB");
    }
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();