namespace CleanArchitecture.Identity.Configurations;

public class UserRoleConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
            {
                RoleId = "55ef9fe0-573a-42e1-9207-b97da7058825",
                UserId = "7e8ce125-dba9-4ca3-8a14-9278a4194dfe"
            },
            new IdentityUserRole<string>
            {
                RoleId = "ee0d13c4-82f5-4c6d-b1bc-8399521ae5bc",
                UserId = "c6a02dc4-519e-44e0-b48e-96e8fccf79a2"
            });
    }
}