namespace CleanArchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(new ApplicationUser
            {
                Id = "7e8ce125-dba9-4ca3-8a14-9278a4194dfe",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "admin123."),
            },
            new ApplicationUser
            {
                Id = "c6a02dc4-519e-44e0-b48e-96e8fccf79a2",
                UserName = "juanperez",
                NormalizedUserName = "JUANPEREZ",
                Email = "juanperez@localhost.com",
                NormalizedEmail = "JUANPEREZ@LOCALHOST.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "perez123."),
            });
    }
}