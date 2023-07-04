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
