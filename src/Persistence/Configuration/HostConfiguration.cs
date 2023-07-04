namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder
            .ToTable(TableNames.Hosts);

        builder
            .HasKey(h => h.Id);
    }
}
