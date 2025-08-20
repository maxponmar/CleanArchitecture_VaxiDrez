namespace CleanArchitecture.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<CleanArchitectureIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                b => b.MigrationsAssembly(typeof(CleanArchitectureIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<CleanArchitectureIdentityDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddTransient<IAuthService, AuthService>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var jwtEnvVarName = configuration["JwtSettings:Key"];
            if(jwtEnvVarName == null)
                throw new Exception("JwtKey Environment Variable name is null");
            
            var jwtKey = Environment.GetEnvironmentVariable(jwtEnvVarName);
            if(jwtKey == null)
                throw new Exception("JwtKey is null");
            
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtEnvVarName))
            };
        });
        
        return services;
    }
}