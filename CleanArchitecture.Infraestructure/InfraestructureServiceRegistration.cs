namespace CleanArchitecture.Infraestructure;

public static class InfraestructureServiceRegistration
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StreamerDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IStreamerRepository, StreamerRepository>();
        
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        
        return services;
    }
}