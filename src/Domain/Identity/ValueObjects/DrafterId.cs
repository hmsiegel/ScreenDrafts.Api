namespace ScreenDrafts.Api.Domain.Identity.ValueObjects;

public sealed class DrafterId : AggregateRootId<DefaultIdType>
{
    private DrafterId(DefaultIdType value)
    {
        Value = value;
    }

    private DrafterId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static DrafterId Create(DefaultIdType value)
    {
        return new DrafterId(value);
    }

    public static DrafterId CreateUnique()
    {
        return new DrafterId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
