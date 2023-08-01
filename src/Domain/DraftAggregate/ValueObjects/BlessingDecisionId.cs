namespace ScreenDrafts.Api.Domain.DraftAggregate.ValueObjects;
public sealed class BlessingDecisionId : AggregateRootId<DefaultIdType>
{
    private BlessingDecisionId(DefaultIdType value)
    {
        Value = value;
    }

    private BlessingDecisionId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static BlessingDecisionId Create(DefaultIdType value)
    {
        return new BlessingDecisionId(value);
    }

    public static BlessingDecisionId CreateUnique()
    {
        return new BlessingDecisionId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
