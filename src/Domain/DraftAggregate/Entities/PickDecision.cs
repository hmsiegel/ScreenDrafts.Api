namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class PickDecision : Entity<PickDecisionId>
{
    private PickDecision(
        DefaultIdType userId,
        Decision decision)
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
            userId,
            decision);
    }
}
