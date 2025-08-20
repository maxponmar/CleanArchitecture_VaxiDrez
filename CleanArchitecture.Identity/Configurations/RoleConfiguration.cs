namespace CleanArchitecture.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(new IdentityRole
        {
            Id = "55ef9fe0-573a-42e1-9207-b97da7058825",
            Name = "Admin",
            NormalizedName = "ADMIN",
        },new IdentityRole
        {
            Id = "ee0d13c4-82f5-4c6d-b1bc-8399521ae5bc",
            Name = "Operator",
            NormalizedName = "OPERATOR",
        });
    }
}