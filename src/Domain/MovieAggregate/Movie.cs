namespace ScreenDrafts.Api.Domain.MovieAggregate;

public sealed class Movie : AggregateRoot<MovieId, DefaultIdType>, IAuditableEntity
{
    private readonly List<MovieCrewMember> _directors = new();
    private readonly List<MovieCrewMember> _writers = new();
    private readonly List<MovieCrewMember> _producers = new();
    private readonly List<MovieCastMember> _cast = new();

    private Movie(
        MovieId movieId,
        string title,
        string year,
        string imageUrl,
        string imdbUrl)
        : base(movieId)
    {
        Title = title;
        Year = year;
        ImageUrl = imageUrl;
        ImdbUrl = imdbUrl;
    }

    private Movie()
    {
    }

    public string? Title { get; private set; }
    public string? Year { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? ImdbUrl { get; private set; }
    public bool? IsInMarqueeOfFame { get; private set; }

    public IReadOnlyList<MovieCrewMember> Directors => _directors.AsReadOnly();
    public IReadOnlyList<MovieCrewMember> Writers => _writers.AsReadOnly();
    public IReadOnlyList<MovieCrewMember> Producers => _producers.AsReadOnly();
    public IReadOnlyList<MovieCastMember> Cast => _cast.AsReadOnly();

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Movie Create(
        string title,
        string year,
        string imageUrl,
        string imdbUrl)
    {
        var movie = new Movie(
            MovieId.CreateUnique(),
            title,
            year,
            imageUrl,
            imdbUrl);

        movie.AddDomainEvent(new MovieCreatedDomainEvent(
            NewId.NextGuid(),
            movie.Id!.Value));
        return movie;
    }
}
