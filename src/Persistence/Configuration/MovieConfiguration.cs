namespace ScreenDrafts.Api.Persistence.Configuration;
internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable(TableNames.Movies);
        builder.HasKey(x => x.Id);
    }
}
