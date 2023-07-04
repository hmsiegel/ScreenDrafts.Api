namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class DrafterConfiguration : IEntityTypeConfiguration<Drafter>
{
    public void Configure(EntityTypeBuilder<Drafter> builder)
    {
        builder.ToTable(TableNames.Drafters);

        builder.HasKey(d => d.Id);
    }
}
