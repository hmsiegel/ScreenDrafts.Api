using static ScreenDrafts.Api.Persistence.Common.DatabaseConstants;

namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class DraftConfiguation : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> builder)
    {
        ConfigueDraftsTable(builder);
        ConfigureDraftHostIdsTable(builder);
        ConfigureDraftDrafterIdsTable(builder);
        ConfigureSelectedMoviesTable(builder);
    }

    private static void ConfigueDraftsTable(EntityTypeBuilder<Draft> builder)
    {
        builder.ToTable(TableNames.Drafts);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id!.Value,
            value => DraftId.Create(value));

        builder.Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.DraftType)
            .HasConversion(
                dt => dt.Name,
                dt => DraftType.FromName(dt, false));
    }

    private static void ConfigureDraftHostIdsTable(EntityTypeBuilder<Draft> builder)
    {
        builder.OwnsMany(d => d.HostIds, hi =>
        {
            hi.ToTable(TableNames.DraftHostIds);
            hi.WithOwner().HasForeignKey(ObjectNames.DraftId);

            hi.HasKey(Id);

            hi.Property(d => d.Value)
                .HasColumnName(ObjectNames.HostId)
                .ValueGeneratedNever();
        });
    }

    private static void ConfigureDraftDrafterIdsTable(EntityTypeBuilder<Draft> builder)
    {
        builder.OwnsMany(d => d.DrafterIds, hi =>
        {
            hi.ToTable(TableNames.DraftDrafterIds);
            hi.WithOwner().HasForeignKey(ObjectNames.DraftId);

            hi.HasKey(Id);

            hi.Property(d => d.Value)
                .HasColumnName(ObjectNames.DrafterId)
                .ValueGeneratedNever();
        });
    }

    private static void ConfigureSelectedMoviesTable(EntityTypeBuilder<Draft> builder)
    {
        builder.OwnsMany(d => d.SelectedMovies, sm =>
        {
            sm.ToTable(TableNames.SelectedMovies);

            sm.WithOwner().HasForeignKey(ObjectNames.DraftId);

            sm.HasKey(Id, ObjectNames.DraftId);

            sm.Property(x => x.Id)
                .HasColumnName(ObjectNames.SelectedMovieId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => SelectedMovieId.Create(value));

            sm.Property(m => m.MovieId)
                .HasColumnName(ObjectNames.MovieId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => MovieId.Create(value));

            sm.OwnsMany(m => m.PickDecisions, pd =>
            {
                pd.ToTable(TableNames.PickDecisions);

                pd.WithOwner().HasForeignKey(ObjectNames.SelectedMovieId, ObjectNames.DraftId);

                pd.HasKey(Id, ObjectNames.DraftId, ObjectNames.SelectedMovieId);

                pd.Property(pd => pd.Id)
                    .HasColumnName(ObjectNames.PickDecisionId)
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id!.Value,
                    value => PickDecisionId.Create(value));

                pd.Property(x => x.Decision)
                                   .HasConversion(
                    d => d.Name,
                    d => Decision.FromName(d, false));

                pd.Property(x => x.UserId)
                    .HasColumnName(ObjectNames.UserId)
                    .ValueGeneratedNever();
            });

            sm.Navigation(x => x.PickDecisions).Metadata.SetField(ObjectNames._pickDecisions);
            sm.Navigation(x => x.PickDecisions).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Draft.SelectedMovies))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
