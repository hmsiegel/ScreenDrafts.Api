namespace ScreenDrafts.Api.Domain.MovieAggregate;

public sealed class Movie : AggregateRoot, IAuditableEntity
{
    private readonly List<string?> _writers = new();
    private readonly List<string?> _draftsSelectedInIds = new();
    private readonly List<Draft> _draftsSelectedIn = new();

    private Movie(
        string? title,
        string? year,
        string? director,
        string? imageUrl,
        string? imdbUrl)
    {
        Title = title;
        Year = year;
        Director = director;
        ImageUrl = imageUrl;
        ImdbUrl = imdbUrl;
    }

    private Movie()
    {
    }

    public string? Title { get; private set; }
    public string? Year { get; private set; }
    public string? Director { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? ImdbUrl { get; private set; }
    public bool? IsInMarqueeOfFame { get; private set; }
    public IReadOnlyList<string?>? Writers => _writers.AsReadOnly();
    public IReadOnlyList<string?>? DraftsSelectedInIds => _draftsSelectedInIds.AsReadOnly();
    public IReadOnlyList<Draft>? DraftsSelectedIn => _draftsSelectedIn.AsReadOnly();

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Movie Create(
        string? title,
        string? year,
        string? director,
        string? imageUrl,
        string? imdbUrl)
    {
        var movie = new Movie(title, year, director, imageUrl, imdbUrl);
        movie.RaiseDomainEvent(new MovieCreatedDomainEvent(NewId.NextGuid(),movie.Id));
        return movie;
    }

    public void AddWriter(string? writer)
    {
        if (string.IsNullOrWhiteSpace(writer))
        {
            throw new ArgumentException("Writer cannot be null.");
        }

        if (_writers.Contains(writer))
        {
            throw new ArgumentException("Writer already exists.");
        }

        _writers.Add(writer);
    }

    public void AddDraftSelectedIn(Draft draft)
    {
        if (draft is null)
        {
            throw new ArgumentNullException(nameof(draft));
        }

        _draftsSelectedIn.Add(draft);
        _draftsSelectedInIds.Add(draft.Id);
    }

    public void AddDraftVetoedIn(Draft draft)
    {
        if (draft is null)
        {
            throw new ArgumentNullException(nameof(draft));
        }

        _draftsSelectedIn.Remove(draft);
        _draftsSelectedInIds.Remove(draft.Id);
    }
}
