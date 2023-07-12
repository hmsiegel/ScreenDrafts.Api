namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class DraftConfiguation : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> builder)
    {
        builder.ToTable(TableNames.Drafts);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DraftType)
            .HasConversion(
                dt => dt.Name,
                value => DraftType.FromName(value, true)!);
    }
}
