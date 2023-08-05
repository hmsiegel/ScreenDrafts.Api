namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        ConfigureMoviesTable(builder);
        ConfigureMovieDirectorsTable(builder);
        ConfigureMovieWritersTable(builder);
        ConfigureMovieProducersTable(builder);
        ConfigureMovieCastTable(builder);
    }

    private static void ConfigureMoviesTable(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable(TableNames.Movies);

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id!.Value,
            value => MovieId.Create(value));

        builder.Property(m => m.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.ImageUrl)
            .HasMaxLength(256);

        builder.Property(m => m.ImdbUrl)
            .HasMaxLength(256);
    }

    private static void ConfigureMovieDirectorsTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.Directors, d =>
        {
            d.ToTable(TableNames.MovieDirectors);

            d.WithOwner().HasForeignKey(ObjectNames.MovieId);

            d.HasKey(Id, ObjectNames.MovieId);

            d.Property(md => md.Id)
                .HasColumnName(ObjectNames.DirectorId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => MovieCrewMemberId.Create(value));

            d.Property(md => md.CrewMemberId)
                .HasConversion(
                    id => id!.Value,
                    value => CrewMemberId.Create(value));
        });

        builder.Metadata.FindNavigation(nameof(Movie.Directors))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMovieWritersTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.Writers, d =>
        {
            d.ToTable(TableNames.MovieWriters);

            d.WithOwner().HasForeignKey(ObjectNames.MovieId);

            d.HasKey(Id, ObjectNames.MovieId);

            d.Property(md => md.Id)
                .HasColumnName(ObjectNames.WriterId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => MovieCrewMemberId.Create(value));

            d.Property(md => md.CrewMemberId)
                .HasConversion(
                    id => id!.Value,
                    value => CrewMemberId.Create(value));
        });

        builder.Metadata.FindNavigation(nameof(Movie.Writers))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMovieProducersTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.Producers, d =>
        {
            d.ToTable(TableNames.MovieProducers);

            d.WithOwner().HasForeignKey(ObjectNames.MovieId);

            d.HasKey(Id, ObjectNames.MovieId);

            d.Property(md => md.Id)
                .HasColumnName(ObjectNames.ProducerId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => MovieCrewMemberId.Create(value));

            d.Property(md => md.CrewMemberId)
                .HasConversion(
                    id => id!.Value,
                    value => CrewMemberId.Create(value));
        });

        builder.Metadata.FindNavigation(nameof(Movie.Producers))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMovieCastTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.Cast, d =>
        {
            d.ToTable(TableNames.MovieCast);

            d.WithOwner().HasForeignKey(ObjectNames.MovieId);

            d.HasKey(Id, ObjectNames.MovieId);

            d.Property(md => md.Id)
                .HasColumnName(ObjectNames.MovieCastId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id!.Value,
                value => MovieCastMemberId.Create(value));

            d.Property(md => md.CastMemberId)
                .HasConversion(
                    id => id!.Value,
                    value => CastMemberId.Create(value));
        });

        builder.Metadata.FindNavigation(nameof(Movie.Cast))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
