namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class DraftConfiguation : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> builder)
    {
        ConfigueDraftsTable(builder);
        ConfigureDraftHostIdsTable(builder);
        ConfigureDraftDrafterIdsTable(builder);
        ConfigurePicksTable(builder);
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

    private static void ConfigurePicksTable(EntityTypeBuilder<Draft> builder)
    {
        builder.OwnsMany(d => d.Picks, sm =>
        {
            sm.ToTable(TableNames.Picks);

            sm.WithOwner().HasForeignKey(ObjectNames.DraftId);

            sm.HasKey(Id, ObjectNames.DraftId);

            sm.Property(x => x.Id)
                .HasColumnName(ObjectNames.PickId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => PickId.Create(value));

            sm.OwnsMany(m => m.PickDecisions, pd =>
            {
                pd.ToTable(TableNames.PickDecisions);

                pd.WithOwner().HasForeignKey(ObjectNames.PickId, ObjectNames.DraftId);

                pd.HasKey(Id, ObjectNames.DraftId, ObjectNames.PickId);

                pd.Property(pd => pd.Id)
                    .HasColumnName(ObjectNames.PickDecisionId)
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id!.Value,
                    value => PickDecisionId.Create(value));

                pd.Property(m => m.MovieId)
                    .HasColumnName(ObjectNames.MovieId)
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id!.Value,
                    value => MovieId.Create(value));

                pd.Property(x => x.DrafterId)
                    .HasColumnName(ObjectNames.DrafterId)
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id!.Value,
                    value => DrafterId.Create(value));

                pd.OwnsMany(pd => pd.BlessingDecisions, bd =>
                {
                    bd.ToTable(TableNames.BlessingDecisions);

                    bd.WithOwner().HasForeignKey(ObjectNames.PickDecisionId, ObjectNames.DraftId, ObjectNames.PickId);

                    bd.HasKey(Id, ObjectNames.DraftId, ObjectNames.PickDecisionId, ObjectNames.PickId);

                    bd.Property(pd => pd.Id)
                        .HasColumnName(ObjectNames.BlessingDecisionId)
                        .ValueGeneratedNever()
                        .HasConversion(
                        id => id!.Value,
                        value => BlessingDecisionId.Create(value));

                    bd.Property(x => x.DrafterId)
                        .HasColumnName(ObjectNames.DrafterId)
                        .ValueGeneratedNever()
                        .HasConversion(
                        id => id!.Value,
                        value => DrafterId.Create(value));

                    bd.Property(x => x.BlessingUsed)
                        .HasConversion(
                        bu => bu.Name,
                        bu => BlessingUsed.FromName(bu, false));
                });

                pd.Navigation(x => x.BlessingDecisions).Metadata.SetField(ObjectNames._blessingDecisions);
                pd.Navigation(x => x.BlessingDecisions).UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            sm.Navigation(x => x.PickDecisions).Metadata.SetField(ObjectNames._pickDecisions);
            sm.Navigation(x => x.PickDecisions).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Draft.Picks))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
