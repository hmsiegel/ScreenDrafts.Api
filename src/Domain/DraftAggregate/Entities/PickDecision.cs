namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class PickDecision : Entity<PickDecisionId>
{
    private readonly List<BlessingDecision> _blessingDecisions = new();

    private PickDecision(
        PickDecisionId id,
        DrafterId drafterId,
        MovieId movieId)
        : base(id)
    {
        DrafterId = drafterId;
        MovieId = movieId;
    }

    private PickDecision()
    {
    }

    public DrafterId DrafterId { get; private set; }
    public MovieId MovieId { get; private set; }
    public IReadOnlyList<BlessingDecision>? BlessingDecisions => _blessingDecisions.AsReadOnly();

    public static PickDecision Create(
        DrafterId drafterId,
        MovieId movieId)
    {
        return new PickDecision(
            PickDecisionId.CreateUnique(),
            drafterId,
            movieId);
    }

    public void AddBlessingDecision(BlessingDecision blessingDecision)
    {
        _blessingDecisions.Add(blessingDecision);
    }
}
