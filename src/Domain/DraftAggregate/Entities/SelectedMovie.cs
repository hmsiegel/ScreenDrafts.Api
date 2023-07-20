namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class SelectedMovie : Entity<SelectedMovieId>, IAuditableEntity
{
    private readonly List<PickDecision> _pickDecisions = new();

    private SelectedMovie(
        SelectedMovieId id,
        MovieId movieId,
        int draftPosition)
        : base(id)
    {
        MovieId = movieId;
        DraftPosition = draftPosition;
    }

    private SelectedMovie()
    {
    }

    public MovieId MovieId { get; private set; }
    public int DraftPosition { get; private set; }
    public IReadOnlyList<PickDecision> PickDecisions => _pickDecisions.AsReadOnly();

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static SelectedMovie Create(
        MovieId movieId,
        int draftPosition)
    {
        return new SelectedMovie(
            SelectedMovieId.CreateUnique(),
            movieId,
            draftPosition);
    }

    public void AddPickDecision(DefaultIdType userId, Decision decision)
    {
        _pickDecisions.Add(PickDecision.Create(userId, decision));
    }
}
