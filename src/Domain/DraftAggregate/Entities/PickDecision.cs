namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class PickDecision : Entity<PickDecisionId>
{
    private PickDecision(
        PickDecisionId id,
        DefaultIdType userId,
        Decision decision)
        : base(id)
    {
        UserId = userId;
        Decision = decision;
    }

    private PickDecision()
    {
    }

    public DefaultIdType UserId { get; private set; }
    public Decision Decision { get; private set; }

    public static PickDecision Create(
        DefaultIdType userId,
        Decision decision)
    {
        return new PickDecision(
            PickDecisionId.CreateUnique(),
            userId,
            decision);
    }
}
