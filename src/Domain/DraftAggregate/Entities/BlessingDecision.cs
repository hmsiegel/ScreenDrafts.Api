namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class BlessingDecision : Entity<BlessingDecisionId>
{
    private BlessingDecision(
        BlessingDecisionId id,
        DrafterId drafterId,
        BlessingUsed blessingUsed)
        : base(id)
    {
        DrafterId = drafterId;
        BlessingUsed = blessingUsed;
    }

    private BlessingDecision()
    {
    }

    public DrafterId DrafterId { get; private set; }
    public BlessingUsed BlessingUsed { get; private set; }

    public static BlessingDecision Create(
        DrafterId drafterId,
        BlessingUsed blessingUsed)
    {
        return new BlessingDecision(
            BlessingDecisionId.CreateUnique(),
            drafterId,
            blessingUsed);
    }
}
