namespace ScreenDrafts.Api.Domain.DraftAggregate;
public sealed class Draft : AggregateRoot<DraftId, DefaultIdType>, IAuditableEntity
{
    private readonly List<DrafterId> _drafterIds = new();
    private readonly List<SelectedMovie> _selectedMovies = new();
    private readonly List<HostId> _hostIds = new();

    private Draft(
        DraftId id,
        string name,
        DraftType draftType,
        int numberOfDrafters)
        : base(id)
    {
        Name = name;
        DraftType = draftType;
        NumberOfDrafters = numberOfDrafters;
    }

    private Draft()
    {
    }

    public string Name { get; private set; }

    [JsonConverter(typeof(SmartEnumNameConverter<DraftType, int>))]
    public DraftType DraftType { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public int? Runtime { get; private set; }
    public string? EpisodeNumber { get; private set; }
    public int? NumberOfDrafters { get; private set; }
    public int? NumberOfFilms { get; private set; }

    public IReadOnlyList<HostId>? HostIds => _hostIds.AsReadOnly();
    public IReadOnlyList<DrafterId>? DrafterIds => _drafterIds.AsReadOnly();
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
        var draft = new Draft(
            DraftId.CreateUnique(),
            name,
            draftType,
            numberOfDrafters);
        draft.AddDomainEvent(new DraftCreatedDomainEvent(
            NewId.NextGuid(),
            draft.Id!.Value));
        return draft;
    }

    public void AddDrafter(DrafterId drafterId)
    {
        _drafterIds.Add(drafterId);
    }

    public void AddHost(HostId hostId)
    {
        _hostIds.Add(hostId);
    }

    public void AddDraftedMovie(SelectedMovie selectedMovie)
    {
        _selectedMovies.Add(selectedMovie);
    }

    public void UpdateDraft(Draft draft)
    {
        Name = draft.Name;
        DraftType = draft.DraftType;
        ReleaseDate = draft.ReleaseDate;
        Runtime = draft.Runtime;
        EpisodeNumber = draft.EpisodeNumber;
        NumberOfDrafters = draft.NumberOfDrafters;
        NumberOfFilms = draft.NumberOfFilms;
    }

    public void UpdateNumberOfFilms(int numberOfFilms)
    {
        NumberOfFilms = numberOfFilms;
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

    public void UpdateHosts(List<HostId> hostIds)
    {
        _hostIds.Clear();
        _hostIds.AddRange(hostIds);
    }

    public void UpdateDrafters(List<DrafterId> drafterIds)
    {
        _drafterIds.Clear();
        _drafterIds.AddRange(drafterIds);
    }

    public void AddSelectedMovie(SelectedMovie selectedMovie)
    {
        _selectedMovies.Add(selectedMovie);
    }
}
