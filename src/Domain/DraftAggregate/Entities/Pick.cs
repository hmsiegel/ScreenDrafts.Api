namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class Pick : Entity<PickId>
{
    private readonly List<PickDecision> _pickDecisions = new();

    private Pick(PickId id, int draftPosition)
        : base(id)
    {
        DraftPosition = draftPosition;
    }

    private Pick()
    {
    }

    public int DraftPosition { get; private set; }
    public IReadOnlyList<PickDecision> PickDecisions => _pickDecisions.AsReadOnly();

    public static Pick Create(int draftPosition)
    {
        return new Pick(
            PickId.CreateUnique(),
            draftPosition);
    }

    public void AddPickDecision(PickDecision pickDecision)
    {
        _pickDecisions.Add(pickDecision);
    }
}
