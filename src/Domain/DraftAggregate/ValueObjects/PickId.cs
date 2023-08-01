namespace ScreenDrafts.Api.Domain.DraftAggregate.ValueObjects;
public sealed class PickId : ValueObject
{
    private PickId(DefaultIdType value)
    {
        Value = value;
    }

    private PickId()
    {
    }

    public DefaultIdType Value { get; private set; }

    public static PickId Create(DefaultIdType value)
    {
        return new PickId(value);
    }

    public static PickId CreateUnique()
    {
        return new PickId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
