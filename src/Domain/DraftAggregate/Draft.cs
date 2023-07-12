namespace ScreenDrafts.Api.Domain.DraftAggregate;
public sealed class Draft : AggregateRoot, IAuditableEntity
{
    private readonly List<Drafter> _drafters = new();
    private readonly List<SelectedMovie> _selectedMovies = new();
    private readonly List<Host> _hosts = new();

    private Draft(
        string name,
        DraftType draftType,
        int numberOfDrafters)
    {
        Name = name;
        DraftType = draftType;
        NumberOfDrafters = numberOfDrafters;
    }

    private Draft()
    {
    }

    public string Name { get; private set; }
    public DraftType DraftType { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public int? Runtime { get; private set; }
    public string? EpisodeNumber { get; private set; }
    public int? NumberOfDrafters { get; private set; }
    public string? MainHostId { get; private set; }
    public string? CoHostId { get; private set; }

    public IReadOnlyList<Host>? Hosts => _hosts.AsReadOnly();
    public IReadOnlyList<Drafter>? Drafters => _drafters.AsReadOnly();
    public IReadOnlyList<SelectedMovie>? SelectedMovies => _selectedMovies.AsReadOnly();

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Draft Create(
        string name,
        DraftType draftType,
        int numberOfDrafters)
    {
        var draft = new Draft(name, draftType, numberOfDrafters);
        draft.RaiseDomainEvent(new DraftCreatedDomainEvent(NewId.NextGuid(), draft.Id));
        return draft;
    }

    public void AddDrafter(Drafter drafter)
    {
        _drafters.Add(drafter);
    }

    public void AddHost(Host host)
    {
        MainHostId = host.Id;
    }

    public void AddCoHost(Host host)
    {
        CoHostId = host.Id;
    }

    public void AddDraftedMovie(SelectedMovie selectedMovie)
    {
        _selectedMovies.Add(selectedMovie);
    }

    public void UpdateReleaseDate(DateTime releaseDate)
    {
        ReleaseDate = releaseDate;
    }

    public void UpdateRuntime(int runtime)
    {
        Runtime = runtime;
    }

    public void UpdateEpisodeNumber(string episodeNumber)
    {
        EpisodeNumber = episodeNumber;
    }

    public void UpdateNumberOfDrafters(int numberOfDrafters)
    {
        NumberOfDrafters = numberOfDrafters;
    }

    public void UpdateDraftType(DraftType draftType)
    {
        DraftType = draftType;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
