namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class IdentityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .ToTable(TableNames.Users);

        builder
            .Property(u => u.ObjectId)
            .HasMaxLength(256);

        builder
            .HasOne(u => u.Drafter)
            .WithOne(d => d!.User)
            .HasForeignKey<Drafter>(d => d.Id)
            .IsRequired(false);

        builder
            .HasOne(u => u.Host)
            .WithOne(d => d!.User)
            .HasForeignKey<Host>(d => d.Id)
            .IsRequired(false);
    }
}

internal sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder
            .ToTable(TableNames.Roles);
    }
}

internal sealed class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        builder
            .ToTable(TableNames.RoleClaims);
    }
}

internal sealed class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder
            .ToTable(TableNames.UserRoles);
    }
}

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
        builder
            .ToTable(TableNames.UserClaims);
}

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder
            .ToTable(TableNames.UserLogins);
}

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder
            .ToTable(TableNames.UserTokens);
}
