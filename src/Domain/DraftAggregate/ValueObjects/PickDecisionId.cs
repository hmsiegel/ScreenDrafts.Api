namespace ScreenDrafts.Api.Domain.DraftAggregate.ValueObjects;
public sealed class PickDecisionId : ValueObject
{
    private PickDecisionId(DefaultIdType value)
    {
        Value = value;
    }

    private PickDecisionId()
    {
    }

    public DefaultIdType Value { get; private set; }

    public static PickDecisionId Create(DefaultIdType value)
    {
        return new PickDecisionId(value);
    }

    public static PickDecisionId CreateUnique()
    {
        return new PickDecisionId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
