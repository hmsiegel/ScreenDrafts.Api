using static ScreenDrafts.Api.Persistence.Common.DatabaseConstants;

namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class DrafterConfiguration : IEntityTypeConfiguration<Drafter>
{
    public void Configure(EntityTypeBuilder<Drafter> builder)
    {
        builder.ToTable(TableNames.Drafters);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id!.Value,
            id => DrafterId.Create(id));
    }
}
