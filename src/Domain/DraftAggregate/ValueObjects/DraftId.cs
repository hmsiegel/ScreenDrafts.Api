namespace ScreenDrafts.Api.Domain.DraftAggregate.ValueObjects;
public sealed class DraftId : AggregateRootId<DefaultIdType>
{
    private DraftId(DefaultIdType value)
    {
        Value = value;
    }

    private DraftId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static DraftId Create(DefaultIdType value)
    {
        return new DraftId(value);
    }

    public static DraftId CreateUnique()
    {
        return new DraftId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
