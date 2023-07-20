using static ScreenDrafts.Api.Persistence.Common.DatabaseConstants;

namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class CastConfiguration : IEntityTypeConfiguration<CastMember>
{
    public void Configure(EntityTypeBuilder<CastMember> builder)
    {
        builder.ToTable(TableNames.CastMembers);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id!.Value,
            value => CastMemberId.Create(value));

        builder.Property(c => c.ImdbId)
            .HasMaxLength(50);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}

internal sealed class CrewConfiguration : IEntityTypeConfiguration<CrewMember>
{
    public void Configure(EntityTypeBuilder<CrewMember> builder)
    {
        builder.ToTable(TableNames.CrewMembers);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id!.Value,
            value => CrewMemberId.Create(value));

        builder.Property(c => c.ImdbId)
            .HasMaxLength(50);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
